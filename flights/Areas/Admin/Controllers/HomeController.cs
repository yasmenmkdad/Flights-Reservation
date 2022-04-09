using flights.Areas.Identity.Data;
using flights.Areas.Identity.Pages.Account;
using flights.Context;
using flights.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace flights.Areas.Admin.Controllers
{
  
    [Area("Admin")]
   //[AllowAnonymous]
   // [Authorize(Roles = "administrator")]
   [Authorize(Policy = "RequireAdministratorRole")]

    public class HomeController : Controller
    {
        flightsContext context;
        FlightsystemdbContext contextflight = new FlightsystemdbContext();

        private readonly UserManager<flightsUser> _userManager;
        public IflightRepositary flightRepositary { get; set; }
        public ITicketRepositary ticketRepositary { get; set; }
        public IAirlineRepositary airlineRepositary { get; set; }
        public IairplaneRepositary airplaneRepositary { get; set; }
        public IcountryRepositary icountryRepositary { get; set; }

        public IUserRepositary userRepositary { get; set; }


        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [MaxLength(12)]
            [MinLength(1)]
            [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
            public string card_number { get; set; }
            [Required]
            [Display(Name = "User Name")]

            public string? User_Name { get; set; }
            [DataType(DataType.PhoneNumber)]
            public string? phone { get; set; }
            public int age { get; set; }
            public string? Address { get; set; }
            [EnumDataType(typeof(Gender))]
            public Gender gender { get; set; }
            [Required]
            public string passport { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        public HomeController(flightsContext context, 
            UserManager<flightsUser> userManager,
            IflightRepositary flightRepositary,
            ITicketRepositary ticketRepositary,
            IAirlineRepositary airlineRepositary,
            IairplaneRepositary airplaneRepositary,
            IUserRepositary userRepositary,
            IcountryRepositary icountryRepositary


            ) { 
        
        this.context=context;
            this._userManager = userManager;

            this.flightRepositary = flightRepositary;
            this.ticketRepositary = ticketRepositary;
            this.airlineRepositary = airlineRepositary;
            this.airplaneRepositary = airplaneRepositary;
            this.userRepositary = userRepositary;
            this.icountryRepositary = icountryRepositary;
        }

        [HttpGet]
        public IActionResult Index2()
        {
            ViewBag.count= contextflight.visitors.Count();
            ViewBag.flightscount = flightRepositary.GetAll().Count;
            ViewBag.usercount = userRepositary.GetAll().Count;


            return View(icountryRepositary.GetAll());
        }
      
        public IActionResult create()
        {
      


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(InputModel flightsUser)

        {
            if (ModelState.IsValid)
            {
                
                flightsUser newuAdmin = new flightsUser();
                newuAdmin.User_Name = flightsUser.User_Name;
                newuAdmin.UserName = flightsUser.Email;

                newuAdmin.Email = flightsUser.Email;
                newuAdmin.phone = flightsUser.phone;
                newuAdmin.age = flightsUser.age;
                newuAdmin.Address = flightsUser.Address;
                newuAdmin.passport = flightsUser.passport;

                newuAdmin.card_number = flightsUser.card_number;
                newuAdmin.gender = flightsUser.gender;
                newuAdmin.EmailConfirmed = true;
               var user= await _userManager.FindByEmailAsync(flightsUser.Email);
                if (user == null)
                {
                    var result = await _userManager.CreateAsync(newuAdmin, flightsUser.Password);
                    var resrole = await _userManager.AddToRoleAsync(newuAdmin, "administrator");

                    //context.Add(newuAdmin);
                    //context.SaveChanges();
                    return RedirectToAction("index2");
                }
                else {
                    ViewBag.emailexist = "email is exist";
                    ModelState.AddModelError("Email", "email is exist");

                }
            }
            return View();
        }
        public IActionResult tables()
        {
          ViewBag.countflight=  flightRepositary.GetAll().Count();
            ViewBag.countticket = ticketRepositary.GetAll().Count();
            ViewBag.countairplane = airplaneRepositary.GetAll().Count();
            ViewBag.countairline = airlineRepositary.GetAll().Count();


            return View();
        }
    
      
       
        


    }
}
