using System.Collections.Generic;
using System;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.API.DTOs
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; }
        public OrderAddress ShippingAddress { get; set; }
        //public DeliveryMethod DeliveryMethod { get; set; } //navigational prop [one]

        public string DeliveryMethod {  get; set; }
        public decimal DeliveryMethodCost {  get; set; }
        public ICollection<OrderItemDTO> Items { get; set; } //[many]
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal Total {  get; set; }
      
    }
}
