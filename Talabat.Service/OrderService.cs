using Microsoft.AspNetCore.Mvc;
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

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethod;
        //private readonly IGenericRepository<Order> _ordersRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(//IBasketRepository basketRepo, IGenericRepository<Product> productRepo,
                            //IGenericRepository<DeliveryMethod> deliveryMethod,
                            //IGenericRepository<Order> ordersRepo,
                            IUnitOfWork unitOfWork,
                            IPaymentService paymentService)
        {
           //_basketRepo = basketRepo;
           // _productRepo = productRepo;
           // _deliveryMethod = deliveryMethod;
           // _ordersRepo = ordersRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, OrderAddress shippingAddress)
        {
            //1- Get Basket from Baskets Repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            //2- Get selected Items at basket from products Repo
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            //3- Calculate Sub total
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity); 

            //4- Get delivery method from deliveryMethods Repo
            var deliverMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //5- Create Order
            var spec = new OrderByPaymentIntentidWithSpec(basket.PaymentIntentid);
            var existingorder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            
            if(existingorder != null)
            {
                 _unitOfWork.Repository<Order>().Delete(existingorder);
                  await _paymentService.CreateorUpdatePaymentIntent(basketId);
            }
            var order = new Order(buyerEmail,shippingAddress,deliverMethod,orderItems,subTotal,basket.PaymentIntentid);
            await _unitOfWork.Repository<Order>().CreateAsync(order);

            //6- Save Changes to DB
            var result = await _unitOfWork.Complete();
            if(result<= 0) return null;
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrdersByIdForUsersAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpec( orderId,buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUsersAsync(string buyerEmail)
        {
            var spec =new OrderWithItemsAndDeliveryMethodSpec(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync(string buyerEmail)
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

     }
}
