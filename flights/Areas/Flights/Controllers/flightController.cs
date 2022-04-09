using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using flights.Services;
using flights.Context;
using flights.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using Newtonsoft.Json;

namespace flights.Areas.Flights.Controllers
{
    [Area("Flights")]

        [Authorize]
    public class flightController : Controller
    {
        countryRepoService countryRepoService = new countryRepoService(new FlightsystemdbContext());
        flightRepoService flightRepoService = new flightRepoService(new FlightsystemdbContext());
        TicketRepoService ticketRepoService = new TicketRepoService(new FlightsystemdbContext());
        airplaneRepoService airplaneRepoService = new airplaneRepoService(new FlightsystemdbContext());


        [AllowAnonymous]
        public IActionResult show()
        {
            List<flight> flights = flightRepoService.GetAll().Where(x=>x.AvailableSeat > 0 /*&& x.DepartureTime > DateTime.Now*/).ToList();
            flights = flights.OrderBy(x=>x.DepartureTime).ToList();



            //country ToCountry = countryRepoService.GetAll().Where(x => x.ID == flights[0].CountryID).FirstOrDefault();
            //ViewBag.Country1 = new { CountryImg = ToCountry.Image, flight = flights[0] };
            
            //ToCountry = countryRepoService.GetAll().Where(x => x.ID == flights[1].CountryID).FirstOrDefault();
            //ViewBag.Country2 = new { CountryImg = ToCountry.Image, flight = flights[1] };

            //ToCountry = countryRepoService.GetAll().Where(x => x.ID == flights[2].CountryID).FirstOrDefault();
            //ViewBag.Country3 = new { CountryImg = ToCountry.Image, flight = flights[2] };

            //ToCountry = countryRepoService.GetAll().Where(x => x.ID == flights[3].CountryID).FirstOrDefault();
            //ViewBag.Country4 = new { CountryImg = ToCountry.Image, flight = flights[3] };

            return View(flights);
        }

        public IActionResult Info(string UserID)
        {


            var ticket = ticketRepoService.GetAll().Where(w => w.UserID == UserID).FirstOrDefault();
            if (ticket == null)
            {
                ViewBag.fail = "No Tickets";

            }
            else
            {
                ViewBag.flight = flightRepoService.GetAll().Where(w => w.FlightNumber == ticket.FlightNumber).FirstOrDefault();
                ViewBag.airplane = airplaneRepoService.GetAll().Where(w => w.FlightNumber == ticket.FlightNumber).FirstOrDefault();
            }
                return View(ticket);
        }

        public IActionResult Booking()
        {
            return View(flightRepoService.GetAll());
        }
        [AllowAnonymous]
        public IActionResult ConfirmBooking(IFormCollection formcollection)
        {
            var flights = flightRepoService.GetAll();
            bool flag = false;
            int flightNumber;
            Entity.flight? Currflight = null;
            int NoOfSeats = int.Parse(formcollection["NoOfSeats"]);
            int year = DateOnly.Parse(formcollection["DepartureTime"]).Year;
            int mon = DateOnly.Parse(formcollection["DepartureTime"]).Month;
            int day = DateOnly.Parse(formcollection["DepartureTime"]).Day;
            int year2 = DateOnly.Parse(formcollection["ArrivalTime"]).Year;
            int mon2 = DateOnly.Parse(formcollection["ArrivalTime"]).Month;
            int day2 = DateOnly.Parse(formcollection["ArrivalTime"]).Day;


            Currflight = flights.Where(x => formcollection["from"] == x.From && formcollection["to"] == x.To && (year == x.DepartureTime.Year && mon == x.DepartureTime.Month && day == x.DepartureTime.Day) && (year2 == x.ArrivalTime.Year && mon2 == x.ArrivalTime.Month && day2 == x.ArrivalTime.Day) && int.Parse(formcollection["NoOfSeats"]) <= x.AvailableSeat).FirstOrDefault();
            if (Currflight != null)
            {
                flightNumber = Currflight.FlightNumber;
                var tickets = ticketRepoService.GetAll();
                ViewBag.flightnum = flightNumber;
                ViewBag.Ticket = (from t in tickets where t.FlightNumber == flightNumber select t).FirstOrDefault();
                ViewBag.NoOfSeats = NoOfSeats;

                return View(Currflight);
            }
            else

                return View("Booking", flightRepoService.GetAll());
        }
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Ticket(int fn)
        {
            ViewBag.flight = flightRepoService.GetAll().Where(w => w.FlightNumber == fn).FirstOrDefault();
            ViewBag.airplane = airplaneRepoService.GetAll().Where(w => w.FlightNumber == fn).FirstOrDefault();

            return View(ticketRepoService.GetAll().Where(w => w.FlightNumber == fn).LastOrDefault());
        }


        [AllowAnonymous]
        public IActionResult Search()
        {
            //SelectList countries = new SelectList(countryRepoService.GetAll(), "ID", "Name");
            SelectList countries = new SelectList(countryRepoService.GetAll(), "Name", "Name");
            ViewBag.countries = countries;
            ViewBag.trips = null;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Search(IFormCollection SearchForm)
        {
            SelectList countries = new SelectList(countryRepoService.GetAll(), "Name", "Name");
            ViewBag.countries = countries;


            List<flight> flights = flightRepoService.GetAll();

            string FromCountry = SearchForm["FromCountry"];
            if (FromCountry != "")
                flights = flights.Where(x => x.From == FromCountry).ToList();

            string ToCountry = SearchForm["ToCountry"];
            if (ToCountry != "")
                flights = flights.Where(x => x.To == ToCountry).ToList();

            string arrivalstr = SearchForm["ArrivalTime"];
            if (arrivalstr != "")
            {
                DateTime arrival = DateTime.Parse(SearchForm["ArrivalTime"]);
                flights = flights.Where(x => x.ArrivalTime.Year == arrival.Year && x.ArrivalTime.Month == arrival.Month && x.ArrivalTime.Day == arrival.Day).ToList();
            }
            string DepartureStr = SearchForm["DepartureTime"];
            if (DepartureStr != "")
            {
                DateTime Departure = DateTime.Parse(SearchForm["DepartureTime"]);
                flights = flights.Where(x => x.DepartureTime.Year == Departure.Year && x.DepartureTime.Month == Departure.Month && x.DepartureTime.Day == Departure.Day).ToList();
            }


            string AvailableSeats = SearchForm["AvailableSeats"];
            if (AvailableSeats == "True")
            {
                flights = flights.Where(x => x.AvailableSeat > 0).ToList();
            }


            ViewBag.trips = flights;
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Gallery()
        {

            ViewBag.countries = countryRepoService.GetAll();

            return View();
        }
        public IActionResult Cancel(int TicketID)
        {
            var ticket = ticketRepoService.GetDetails(TicketID);
            var flight = flightRepoService.GetAll().FirstOrDefault(x => x.FlightNumber == ticket.FlightNumber);
            flight.AvailableSeat += ticket.NoOfSeats;
            flightRepoService.Updateflight(flight.FlightNumber, flight);
            ticketRepoService.DeleteTicket(TicketID);
            return View();
        }

    }
}
