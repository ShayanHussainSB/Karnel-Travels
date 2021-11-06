using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models
{
    public class Tour
    {
        [Key]
        public int Tour_Id { get; set; }

        [Required(ErrorMessage = "Enter Tour Name")]
        public string Tour_Name { get; set; }

        [Required(ErrorMessage = "Enter Country Name ")]
        public string Tour_Country { get; set; }

        [Required(ErrorMessage  = "Enter City Name")]
        public string Tour_City { get; set; }

        [Required(ErrorMessage  = "Enter Tour Organizer's Name")]
        public string Tour_OrganizerName { get; set; }

        [Required(ErrorMessage = "Enter Tour Organizer's Contact")]
        [MinLength(11,ErrorMessage = "Enter A Proper Number")]
        public string Tour_OrganizerContact { get; set; }

        [MinLength(10, ErrorMessage = "Type Atleast 10 Words For Information")]
        [Required(ErrorMessage = "Enter Tour Important Information")]
        public string Tour_ImpInfo { get; set; }

        [Required(ErrorMessage = "Enter Tour Important OverView")]
        public string Tour_OverView { get; set; }

        public string Tour_Picture { get; set; }

        [Required(ErrorMessage = "Enter a Duration")]
        public string Tour_Duration { get; set; }

        [Required(ErrorMessage = "Enter Tour Price")]
        public string Tour_Price { get; set; }

        [MinLength(10, ErrorMessage = "Type Atleast 10 Words For Address")]
        [Required(ErrorMessage = "Enter an Address for Tour")]
        public string Tour_Address { get; set; }

        public ICollection<Tour_Booking> Tour_Bookings { get; set; }



    }
}