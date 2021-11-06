using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karnel_Travels.Models
{
    public class Tour_Booking
    {
        [Key,Column(Order=0)]
        public int User_Id { get; set; }
        [Key, Column(Order = 1)]
        public int Tour_Id { get; set; }

        public int No_Adults { get; set; }

        public int No_Child{ get; set; }

        public string  Price { get; set; }

        [DataType(DataType.DateTime)]
        [checkDateTime]
        public string Date_Book { get; set; }

        public bool Status { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Tour> Tours { get; set; }
    }


    public class checkDateTime : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime userdt = Convert.ToDateTime(value);
            double totaldays = DateTime.Now.Subtract(userdt).TotalDays;

            if(totaldays <= 0)
            {
                return new ValidationResult("Date should be greater than current date");
            }
                else if(totaldays <= 1 )
            {
                return new ValidationResult("Booking Can Be Done After Today");
            }
            
            else
            {
                return ValidationResult.Success;
            }
            
        }
    }


}