using flights.Services;
using Microsoft.AspNetCore.Mvc;
using flights.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace flights.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]


    public class AirlineController : Controller
    {
        public IAirlineRepositary airlineRepositary { get; set; }
        public IairplaneRepositary IairplaneRepositary { get; set; }
        public AirlineController(IAirlineRepositary airlineRepositary ,IairplaneRepositary iairplaneRepositary)
        {
            this.airlineRepositary= airlineRepositary;
            this.IairplaneRepositary= iairplaneRepositary;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DeatailsTableAirline()
        {

            return View(airlineRepositary.GetAll());
        }
        public IActionResult Update(int id)
        {
            var airline = airlineRepositary.GetDetails(id);

            SelectList selectListItems = new SelectList(IairplaneRepositary.GetAll().ToList(), "RegisterationNumber", "RegisterationNumber", airline.RegisterationNumber);
            ViewBag.RegisterationNumber = selectListItems;


            return View(airlineRepositary.GetDetails(id));
        }
        [HttpPost]
        public IActionResult Update(airline airline)
        {
            airlineRepositary.UpdateAirline(airline.Id, airline);

            return RedirectToAction("DeatailsTableAirline");
        }
        public IActionResult Details(int id)
        {
            return View(airlineRepositary.GetDetails(id));
        }
        public IActionResult Delete(int id)
        {
            airlineRepositary.DeleteAirline(id);
            return RedirectToAction("DeatailsTableAirline");
        }
        public IActionResult Create()
        {
          

            SelectList selectListItems = new SelectList(IairplaneRepositary.GetAll().ToList(), "RegisterationNumber", "RegisterationNumber");
            ViewBag.RegisterationNumber = selectListItems;

            return View();
        }
        [HttpPost]
        public IActionResult Create(airline airline)
        {

            airlineRepositary.Insert(airline);
            return RedirectToAction("DeatailsTableAirline");
        }
    }
}
