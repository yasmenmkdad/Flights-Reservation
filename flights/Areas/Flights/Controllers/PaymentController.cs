using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using flights.Services;
using flights.Entity;
using Microsoft.AspNetCore.Identity;
using flights.Areas.Identity.Data;
using Newtonsoft.Json;

namespace flights.Areas.Flights.Controllers
{
    [Area("Flights")]
    [AllowAnonymous]

    public class PaymentController : Controller
    {


        UserManager<flightsUser> UserManager;
        private int amount = 100;
        public IflightRepositary iflightRepositary { get; set; }

        public ITicketRepositary ticketRepositary { get; set; }
        public IairplaneRepositary iairplaneRepositary { get; set; }


        public PaymentController(IairplaneRepositary iairplaneRepositary, IflightRepositary iflightRepositary, ITicketRepositary ticketRepositary ,UserManager<flightsUser> userManager)
        {
            this.ticketRepositary = ticketRepositary;
            this.UserManager = userManager;
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            this.iairplaneRepositary=iairplaneRepositary;
            this.iflightRepositary = iflightRepositary;
        }
        public IActionResult Index()
            {
                ViewBag.PaymentAmount = amount;
                return View("Booking");
            }



        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail ,int fn,int noOfSeats)
        {
            /*Dictionary<string, string> Metadata = new Dictionary<string, string>();
            Metadata.Add("Product", "RubberDuck");
            Metadata.Add("Quantity", "10");*/
            var options = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "USD",
                Description = "Buying 10 rubber ducks",
                //SourceId = stripeToken,
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                //Metadata = Metadata,
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);


            var tickets=ticketRepositary.GetAll();
            var flights = iflightRepositary.GetAll();
            var airplanes = iairplaneRepositary.GetAll();

            var flight = flights.Where(x => x.FlightNumber == fn).FirstOrDefault();
            flight.AvailableSeat -= noOfSeats;

            iflightRepositary.Updateflight(fn,flight);
            ticket ticket = new ticket()
            {
                NoOfSeats = noOfSeats,
                CheckStatus = "Confirm",
                UserID = UserManager.GetUserId(User),
                FlightNumber = fn,
                

            };
            ticketRepositary.Insert(ticket);
          
            return RedirectToAction("Ticket", "flight", new { fn=fn});



        }
        private readonly string WebhookSecret = "whsec_OurSigningSecret";



            //Previous actions



            [HttpPost]
            public IActionResult ChargeChange()
            {
                var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();



                try
                {
                    var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], WebhookSecret, throwOnApiVersionMismatch: true);
                    Charge charge = (Charge)stripeEvent.Data.Object;
                    switch (charge.Status)
                    {
                        case "succeeded":
                        //This is an example of what to do after a charge is successful
                        /*charge.Metadata.TryGetValue("Product", out string Product);
                        charge.Metadata.TryGetValue("Quantity", out string Quantity);*/
                        return View("Gallery");
                            //Database.ReduceStock(Product, Quantity);
                            break;
                        case "failed":
                            //Code to execute on a failed charge
                            break;
                    }
                }
                catch (Exception e)
                {
                    //e.Ship(HttpContext);
                    return BadRequest();
                }
                return Ok();
            }
        }


    }

