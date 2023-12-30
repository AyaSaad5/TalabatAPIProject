using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.IO;
using System;
using System.Threading.Tasks;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Enitites;
using Talabat.Core.Services;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.API.Controllers
{
    public class PaymentController : BaseAPIController
    {
        private readonly IPaymentService _paymentService;
        private const string _whSecret = "whsec_50f8416187dfe5d88bdf4c2e382477929c95579db3b0ae83bdb7f93764277a62";


        public PaymentController(IPaymentService paymentService)
        {
           _paymentService = paymentService;
        }
        [Authorize]

        [HttpPost("{basketId}")] //api/payment/basketId
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateorUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "There is a problem with your basket"));
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _whSecret);

                PaymentIntent intent;
                Order order;
                // Handle the event
                switch(stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        intent = (PaymentIntent) stripeEvent.Data.Object;
                        order = await _paymentService.UpdatePaymentIntentToSuccessedOrFailed(intent.Id,true);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await _paymentService.UpdatePaymentIntentToSuccessedOrFailed(intent.Id,false);
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }



    }
}
