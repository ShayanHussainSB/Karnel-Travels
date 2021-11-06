using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Enter Your FirstName")]
        public string User_FirstName { get; set; }

        [Required(ErrorMessage = "Enter Your Last Name")]
        public string User_LastName { get; set; }

        [EmailAddress(ErrorMessage = "Enter a Proper Email")]
        [Required(ErrorMessage = "Enter Your Email")]
        public string User_Email { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        [MaxLength(15, ErrorMessage = "Your Password must be 7 To 15 Character Long")]
        [MinLength(7, ErrorMessage = "Your Password must be Atleast 7 Character Long")]
        public string User_Password { get; set; }

        [Required(ErrorMessage = "Enter Your Contact")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(11, ErrorMessage = "Your Entered Phone Number Is InCorrect")]
        public string User_Contact { get; set; }

        [Required(ErrorMessage = "Enter Your Gender")]
        public string User_Gender { get; set; }

        [Required(ErrorMessage = "Enter Your Address")]
        public string User_Address { get; set; }
        public string User_Type { get; set; }
    }
}