using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Karnel_Travels.Models
{
    public class Accomadtion
    {
        [Key]
        public int Accomadtion_Id { get; set; }

        [Required(ErrorMessage = "Enter Accomadtion Name")]
        public string Accomadtion_Name { get; set; }

        [Required(ErrorMessage = "Enter Country Name ")]
        public string Accomadtion_Country { get; set; }

        [Required(ErrorMessage = "Enter City Name")]
        public string Accomadtion_City { get; set; }

        [Required(ErrorMessage = "Enter Tour Accomadtion Contact")]
        [MinLength(11, ErrorMessage = "Enter A Proper Number")]
        [MaxLength(11, ErrorMessage = "Enter A Proper Number")]
        public string Accomadtion_Contact { get; set; }

        [Required(ErrorMessage = "Select a Type")]
        public string Accomadtion_Type { get; set; }

        [MinLength(10, ErrorMessage = "Type Atleast 10 Words For Information")]
        [Required(ErrorMessage = "Enter Tour Important Information")]
        public string Accomadtion_ImpInfo { get; set; }

        [MinLength(10, ErrorMessage = "Type Atleast 10 Words For OverView")]
        [Required(ErrorMessage = "Enter Tour Important OverView")]
        public string Accomadtion_OverView { get; set; }

        public string Accomadtion_Picture { get; set; }

        [Required(ErrorMessage = "Enter Accomadtion Price")]
        public string Accomadtion_Price { get; set; }

        [MinLength(10, ErrorMessage = "Type Atleast 10 Words For Address")]
        [Required(ErrorMessage = "Enter an Address for Accomadtion")]
        public string Accomadtion_Address { get; set; }

        public ICollection<Accomadtion_Booking> Accomadtion_Bookings { get; set; }

    }
}
