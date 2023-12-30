using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.API.DTOs
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO ShippiingAddress{ get; set; }

    }
}
