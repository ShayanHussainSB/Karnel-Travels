using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models
{
    public class Travel
    {
        [Key]
        public int Travel_Id { get; set; }

        [Required(ErrorMessage = "Enter A Name For Travel")]
        public string Travel_Name { get; set; }

        [Required(ErrorMessage = "Enter Driver Name")]
        public string Travel_DriverName { get; set; }

        [Required(ErrorMessage = "Enter Details For Travel")]
        public string Travel_Details { get; set; }

        [Required(ErrorMessage = "Enter Driver's Contact No")]
        [MinLength(11,ErrorMessage = "Enter A Proper Contact No")]
        [MaxLength(11, ErrorMessage = "Enter A Proper Contact No")]
        public string Travel_Contact { get; set; }

        [Required(ErrorMessage = "Enter Travel's Price")]
        public string Travel_Price { get; set; }

        public string Travel_Picture { get; set; }

        [Required(ErrorMessage = "Enter Travel's Working City")]
        public string Travel_City { get; set; }

        [Required(ErrorMessage = "Select Type For Travel")]
        public string Travel_Type { get; set; }

        public ICollection<Travel_Booking> Tour_Bookings { get; set; }

    }
}