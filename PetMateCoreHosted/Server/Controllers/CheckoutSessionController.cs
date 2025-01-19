using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetMateCoreHosted.Server.Models;
using Stripe;
using Stripe.Checkout;

namespace PetMateCoreHosted.Server.Controllers
{
    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutSessionController : Controller
    {

        [HttpPost]
        /* This API endpoint creates a new checkout session when requested by the Blazor app. */
        public ActionResult Create([FromBody] SessionCreateRequest request)
        {
            // var domain = "https://localhost:7137";  // API domain for redirection
            // var domain = "http://localhost:5183";
            var domain = "https://localhost:7128";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = request.PriceId,   // Price ID for a product created in the Stripe interface
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = domain + "/success",   // Redirect API endpoint when payment is successful
                CancelUrl = domain + "/cancel",   // Redirect API endpoint when payment is cancelled (Stripe handles failed payments in an infinite loop. Only option to exit is cancelling the checkout process)
            };

            var service = new SessionService();
            Session session = service.Create(options);   // Create checkout session

            return new JsonResult(new { Url = session.Url });  // Return URL for redirection
        }

    }

}

