using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Enitites
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice {  get; set; }
        public string PaymentIntentid { get; set; }
        public string ClientSecret { get; set; }

    }
}
