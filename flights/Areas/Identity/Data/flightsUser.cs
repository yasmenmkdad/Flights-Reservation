using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace flights.Areas.Identity.Data
{
    public enum Gender { Female, Male }
    // Add profile data for application users by adding properties to the flightsUser class
    public class flightsUser : IdentityUser
    {
        [StringLength(14)]
        [MinLength(14)]
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

    }
}
