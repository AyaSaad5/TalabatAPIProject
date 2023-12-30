using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class OrderByPaymentIntentidWithSpec : BaseSpecification<Order>
    {
        public OrderByPaymentIntentidWithSpec(string paymentIntentId)
            : base(O => O.PaymentIntentId == paymentIntentId) 
        {
            
        }
    }
}
