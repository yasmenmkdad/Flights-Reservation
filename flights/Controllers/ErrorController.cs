using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace flights.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpSatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if(statusCode == 404)
                return View("ErrorNotFound");

            return View("ErrorNotHandle");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("ErrorNotHandle");
        }
    }
}
