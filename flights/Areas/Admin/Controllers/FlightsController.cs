using flights.Services;
using Microsoft.AspNetCore.Mvc;
using flights.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace flights.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]

    [Area("Admin")]
    public class FlightsController : Controller
    {
        public IflightRepositary flightRepositary { get; set; }
        public IcountryRepositary IcountryRepositary { get; set; }

        public FlightsController(IflightRepositary iflightRepositary ,IcountryRepositary icountryRepositary)
        {
            this.flightRepositary = iflightRepositary;
            this.IcountryRepositary= icountryRepositary;
        }
      
        public IActionResult DeatailsTable()
        {


            return View(flightRepositary.GetAll());
        }
        public IActionResult Update(int id)
        {
            var flight = flightRepositary.GetDetails(id);

            SelectList CountryID = new SelectList(IcountryRepositary.GetAll().ToList(), "ID", "Name",flight.CountryID );
            ViewBag.CountryID = CountryID;


            return View(flightRepositary.GetDetails(id));
        }
        [HttpPost]
        public IActionResult Update(flight flight)
        {
            flightRepositary.Updateflight(flight.FlightNumber, flight);

            return RedirectToAction("DeatailsTable");
        }
        public IActionResult Details(int id)
        {
            return View(flightRepositary.GetDetails(id));
        }
        public IActionResult Delete(int id)
        {
            flightRepositary.Deleteflight(id);
            return RedirectToAction("DeatailsTable");
        }
        public IActionResult Create()
        {

            SelectList CountryID = new SelectList(IcountryRepositary.GetAll().ToList(), "ID", "Name");
            ViewBag.CountryID = CountryID;
            return View();
        }
        [HttpPost]
        public IActionResult Create(flight flight)
        {

            flightRepositary.Insert(flight);
            return RedirectToAction("DeatailsTable");
        }
    }
}
