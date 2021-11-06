using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karnel_Travels.Models;

namespace Karnel_Travels.Models
{
    public class TourViewModel
    {
        public List<Accomadtion> Accomodations { get; set; }

        public List<Tour> Tours { get; set; }

        public List<Tour> Tours2 { get; set; }
    }
}