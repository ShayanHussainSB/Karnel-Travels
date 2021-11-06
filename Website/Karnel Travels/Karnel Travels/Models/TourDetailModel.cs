using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karnel_Travels.Models;

namespace Karnel_Travels.Models
{
    public class TourDetailModel
    {
        public List<Tour> Tours { get; set; }

        public List<Tour> Toursdata { get; set; }

        public List<Accomadtion> Accomadtions { get; set; }

        public List<Accomadtion> Accomadtionsdata { get; set; }

        public List<Travel> Travel { get; set; }

        public List<Travel> Travelsdata { get; set; }
    }
}