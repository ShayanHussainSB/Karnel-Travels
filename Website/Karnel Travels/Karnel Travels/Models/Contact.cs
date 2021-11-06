using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Enter Your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Your Email Address")]
        [EmailAddress(ErrorMessage = "Enter a Correct Email Address")]
        public string Email { get; set; }

        public string ToEmail { get; set; }
        public string FromEmail { get; set; }

        [Required(ErrorMessage = "Enter a Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Enter A Message")]
        public string Body { get; set; }
    }
}