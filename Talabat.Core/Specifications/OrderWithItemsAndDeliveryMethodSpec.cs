using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class OrderWithItemsAndDeliveryMethodSpec : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpec(string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.OrderDate);
        }
        public OrderWithItemsAndDeliveryMethodSpec(int orderId, string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail && order.Id == orderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
