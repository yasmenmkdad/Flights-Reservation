using flights.Services;
using Microsoft.AspNetCore.Mvc;
using flights.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace flights.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]

    public class AirplaneController : Controller
    {
        public IairplaneRepositary airplaneRepositary { get; set; }
        public IflightRepositary IflightRepositary { get; set; }

        public AirplaneController(IairplaneRepositary iairplaneRepositary ,IflightRepositary iflightRepositary)
        {
            this.airplaneRepositary=iairplaneRepositary;
            this.IflightRepositary=iflightRepositary;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DeatailsTableAirplane()
        {

            return View(airplaneRepositary.GetAll());
        }
        public IActionResult Update(int id)
        {
            var airplane1 = airplaneRepositary.GetDetails(id);

            SelectList selectListItems = new SelectList(IflightRepositary.GetAll().ToList(), "FlightNumber", "FlightNumber", airplane1.FlightNumber);
            ViewBag.FlightNumber = selectListItems;


            return View(airplaneRepositary.GetDetails(id));
        }
        [HttpPost]
        public IActionResult Update(airplane airplane)
        {
            airplaneRepositary.Updateairplane(airplane.RegisterationNumber, airplane);

            return RedirectToAction("DeatailsTableAirplane");
        }
        public IActionResult Details(int id)
        {
            return View(airplaneRepositary.GetDetails(id));
        }
        public IActionResult Delete(int id)
        {
            airplaneRepositary.Deleteairplane(id);
            return RedirectToAction("DeatailsTableAirplane");
        }
        public IActionResult Create()
        {

            SelectList selectListItems = new SelectList(IflightRepositary.GetAll().ToList(), "FlightNumber", "FlightNumber");
            ViewBag.FlightNumber = selectListItems;


            return View();
        }
        [HttpPost]
        public IActionResult Create(airplane airplane)
        {

            airplaneRepositary.Insert(airplane);
            return RedirectToAction("DeatailsTableAirplane");
        }
    }
}
