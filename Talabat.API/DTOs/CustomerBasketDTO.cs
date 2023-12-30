using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Enitites;

namespace Talabat.API.DTOs
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string PaymentIntentid { get; set; }
        public string ClientSecret { get; set; }
    }
}
