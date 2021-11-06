using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter Your Email Address")]
        [EmailAddress(ErrorMessage = "Enter A Correct Emaii Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        public string Password { get; set; }
    }
}