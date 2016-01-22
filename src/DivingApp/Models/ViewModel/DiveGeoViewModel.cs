using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel
{
    public class DiveGeoViewModel
    {
        public long DiveId { get; set; }

        public long DiveNumber { get; set; }

        public string Location { get; set; }

        public string DiveDate { get; set; }

        public string DiveTime { get; set; }

        public string Depth { get; set; }

        public string DiveComment { get; set; }

        public string CoordinateX { get; set; }

        public string CoordinateY { get; set; }

        public bool HasPhotos { get; set; }
    }
}
