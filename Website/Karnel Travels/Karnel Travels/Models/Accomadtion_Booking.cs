using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karnel_Travels.Models
{
    public class Accomadtion_Booking
    {
        [Key, Column(Order = 0)]
        public int User_Id { get; set; }
        [Key, Column(Order = 1)]
        public int Accomadtion_Id { get; set; }

        public int No_Adults { get; set; }

        public int No_Child { get; set; }

        public string Price { get; set; }

        [DataType(DataType.DateTime)]
        [checkDateTime]
        public string Date_Book { get; set; }

        public bool Status { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Accomadtion> Accomadtions { get; set; }
    }
}