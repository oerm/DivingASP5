using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel
{
    public class DiveShortViewModel
    {
        public long DiveID { get; set; }

        public bool Selected { get; set; }

        public decimal DiveNumber { get; set; }

        public string DiveDate { get; set; }

        public double? Depth { get; set; }

        public byte? Time { get; set; }

        public Nullable<decimal> CountryID { get; set; }

        public string CountryName { get; set; }
    }
}
