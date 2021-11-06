using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karnel_Travels.Models
{
    public class AccountViewModel
    {
        public List<Tour_Booking> Tour_Book { get; set; }
        public List<Accomadtion_Booking> Accomadtion_Book { get; set; }
        public List<Travel_Booking> Travel_Books { get; set; }

        public List<User> Users { get; set; }
    }
}