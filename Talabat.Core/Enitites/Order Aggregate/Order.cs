using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Enitites.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } //navigational prop [one]
        public ICollection<OrderItem> Items { get; set; } //[many]
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }

        public Order(string buyerEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod,ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public Order()
        {
            
        }
        public decimal GetTotal()
            => SubTotal = DeliveryMethod.Cost;
    }
}
