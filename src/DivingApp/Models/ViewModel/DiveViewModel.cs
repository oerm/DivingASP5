using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DivingApp.Models.ViewModel
{
    public class DiveViewModel
    {
        public long DiveID { get; set; }
        
        [DefaultValue(1)]
        [Editable(false)]
        public long DiveNumber { get; set; }

        [Required]
        public DateTime DiveDate
        {
            get { return _diveDate; }
            set { _diveDate = value; }
        }
        private DateTime _diveDate = DateTime.Now;

        public string DiveDateString { get; set; }

        [Required]
        public Nullable<long> CountryId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public Nullable<byte> DiveTime { get; set; }
      
        public Nullable<double> Weight { get; set; }

        [Required]
        public Nullable<byte> WeightIsOk { get; set; }

        [Required]
        public Nullable<byte> Tank { get; set; }
      
        public Nullable<byte> TankStart { get; set; }
      
        public Nullable<byte> TankEnd { get; set; }

        public Nullable<double> AirTemperature { get; set; }

        public Nullable<double> WaterTemperature { get; set; }

        public Nullable<byte> Visibility { get; set; }

        public Nullable<byte> TotalMinutes { get; set; }

        public Nullable<double> MaxDepth { get; set; }

        public Nullable<byte> FiveMetersMinutes { get; set; }

        [Required]
        public Nullable<byte> SuitType { get; set; }

        public string Comments { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }
    }
}