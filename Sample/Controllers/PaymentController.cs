using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using Stripe;
using Stripe.Checkout;

namespace Sample.Controllers
{

    [Route("create-checkout-session")]
    [ApiController]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charge(string stripeEmail,string stripeToken)
        {
            var customers = new CustomerService();
            var charges= new ChargeService();
            var cards= new CardService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
                
                
            });
            var card = new CardCreateOptions();

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 100,
                Description = "Purchase receipt",
                Currency = "CAD",
                Customer = customer.Id,
                ReceiptEmail= stripeEmail,
                
                
            });
            if(charge.Status=="Succeeded")
            {
                string balanceTransactionId = charge.BalanceTransactionId;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
           
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var domain = "http://localhost:5226";

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    
                    PriceData = new SessionLineItemPriceDataOptions
                     {
                           Currency = "cad",
                           UnitAmount = 5000,
                           ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                               
                           Name = "Your cart",
                             Description = "Purchase details",
                         },
                      },
                    Quantity = 1,
                  },
                },
                Mode = "payment",

                SuccessUrl = baseUrl + "/Home/Success",
                CancelUrl = baseUrl + "/Home/Cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            

            Response.Headers.Add("Location", session.Url);
           
            return new StatusCodeResult(303);
        }
    }
}

