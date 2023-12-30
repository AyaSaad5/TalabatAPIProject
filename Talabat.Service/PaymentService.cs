using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;
using Talabat.Core.Enitites.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;
using Product = Talabat.Core.Enitites.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
                              IBasketRepository basketRepo,
                              IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateorUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepo.GetBasketAsync(basketId);
            if(basket == null) return null;

            var shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
                basket.ShippingPrice = shippingPrice;
            }

            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price != product.Price) 
                    item.Price = product.Price;
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentid))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long) shippingPrice,
                    Currency = "usd",
                    PaymentMethodTypes = new List <string>() { "card"}
                };
                intent = await service.CreateAsync(options);

                basket.PaymentIntentid = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * item.Price * 100)+ (long)shippingPrice
                };
                await service.UpdateAsync(basket.PaymentIntentid, options);
            }

            await _basketRepo.UpdatebasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSuccessedOrFailed(string paymentIntentId, bool IsSuccessed)
        {
            var spec = new OrderByPaymentIntentidWithSpec(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if (IsSuccessed)
                order.Status = OrderStatus.PaymentRecieved;
            else
                order.Status = OrderStatus.PaymentFailed;

            _unitOfWork.Repository<Order>().Updater(order);
            await _unitOfWork.Complete();

            return order;

        }
    }
}
