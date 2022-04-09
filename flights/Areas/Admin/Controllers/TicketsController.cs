using flights.Entity;
using flights.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace flights.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]

    public class TicketsController : Controller
    {
        public IflightRepositary IflightRepositary { get; set; }

        public ITicketRepositary ticketRepositary { get; set; }
        public IUserRepositary userRepositary { get; set; }



        public TicketsController(IUserRepositary userRepositary, ITicketRepositary ticketRepositary ,IflightRepositary iflightRepositary)
        {
            this.ticketRepositary = ticketRepositary;
            this.IflightRepositary= iflightRepositary;
            this.userRepositary = userRepositary;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DeatailsTableTicket()
        {
            return View(ticketRepositary.GetAll());
        }
        public IActionResult Update(int id) {
            var ticket = ticketRepositary.GetDetails(id);

            SelectList FlightNumber = new SelectList(IflightRepositary.GetAll().ToList(),"FlightNumber", "FlightNumber",ticket.FlightNumber);
            ViewBag.FlightNumber = FlightNumber;
            ViewBag.UserID = new SelectList(userRepositary.GetAll().ToList(),"Id", "Id",ticket.UserID);
            return View(ticketRepositary.GetDetails(id));
        }
        [HttpPost]
        public IActionResult Update(ticket ticket)
        {
            ticketRepositary.UpdateTicket(ticket.Id, ticket);

            return RedirectToAction("DeatailsTableTicket");
        }
        public IActionResult Details(int id) { 
            return View(ticketRepositary.GetDetails(id));
        }
        public IActionResult Delete(int id)
        {
            ticketRepositary.DeleteTicket(id);
            return RedirectToAction("DeatailsTableTicket");
        }
        public IActionResult Create() {
            SelectList FlightNumber = new SelectList(IflightRepositary.GetAll().ToList(), "FlightNumber", "FlightNumber");
            ViewBag.FlightNumber = FlightNumber;
            ViewBag.UserID = new SelectList(userRepositary.GetAll().ToList(), "Id", "Id");

            return View();
        }
        [HttpPost]
        public IActionResult Create(ticket ticket)
        {

            ticketRepositary.Insert(ticket);
            return RedirectToAction("DeatailsTableTicket");
        }
    }
}
