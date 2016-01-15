using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel
{
    public class DiveGeoViewModel
    {
        public decimal DiveId { get; set; }

        public string Location { get; set; }

        public DateTime DiveDate { get; set; }

        public string DiveComment { get; set; }

        public string CoordinateX { get; set; }

        public string CoordinateY { get; set; }
    }
}
