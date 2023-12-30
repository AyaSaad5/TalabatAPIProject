using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateorUpdatePaymentIntent(string basketId);
        Task<Order> UpdatePaymentIntentToSuccessedOrFailed(string paymentIntentId, bool IsSuccessed);
    }
}
