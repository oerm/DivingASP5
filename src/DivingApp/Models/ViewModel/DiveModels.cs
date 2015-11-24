using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DivingApp.Models.DataModel
{
    public class DiveModel
    {
        public decimal DiveID { get; set; }
        
        [DefaultValue(1)]
        [Display(Name = "№ погружения")]
        [Editable(false)]
        public decimal DiveNumber { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Дата погружения должен быть в формате Дата")]
        [Display(Name = "Дата погружения")]
        public DateTime DiveDate
        {
            get { return _diveDate; }
            set { _diveDate = value; }
        }
        private DateTime _diveDate = DateTime.Now;

        [Required]
        [Display(Name = "Страна")]
        public Nullable<decimal> Country { get; set; }

        [Display(Name = "Место погружения")]
        public string Location { get; set; }

        [Display(Name = "Когда погружались")]
        public Nullable<byte> DiveType { get; set; }

        [Display(Name = "Вес груза (кг.)")]
        public Nullable<double> Weight { get; set; }
        public Nullable<byte> WeightIsOk { get; set; }
        
        [Display(Name = "Объем баллона ")]
        public Nullable<byte> Tank { get; set; }

        [Display(Name = "PSI в начале")]
        public Nullable<byte> TankStart { get; set; }

        [Display(Name = "PSI в конце")]
        public Nullable<byte> TankEnd { get; set; }

        [Display(Name = "Темпрература воздуха")]
        public Nullable<double> AirTemperature { get; set; }

        [Display(Name = "Темпрература воды")]
        public Nullable<double> WaterTemperature { get; set; }

        [Display(Name = "Видимость (м)")]
        public Nullable<byte> Visibility { get; set; }

        [Display(Name = "Время погружения (мин.)")]
        public Nullable<byte> TotalMinutes { get; set; }

        [Display(Name = "Макс. глубина")]
        public Nullable<double> MaxDepth { get; set; }

        [Display(Name = "Остановка безопасности (мин.)")]
        public Nullable<byte> FiveMetersMinutes { get; set; }

        [Display(Name = "Тип костюма")]
        public Nullable<byte> SuitType { get; set; }

        [Display(Name = "Комментарии")]
        public string Comments { get; set; }

        [Display(Name = "Широта")]
        public double? Latitude { get; set; }

        [Display(Name = "Долгота")]
        public double? Longitude { get; set; }

        public static explicit operator DiveModel(Dive dive)
        {
            return new DiveModel()            
            {
                DiveID = dive.DiveID,             
                DiveDate = dive.DiveDate,
                Country = dive.Country,
                Location = dive.Location,
                Weight = dive.Weight,
                WeightIsOk = dive.WeightIsOk,
                Tank = dive.Tank,
                DiveType= dive.DiveType,
                TankStart = dive.TankStart,
                TankEnd = dive.TankEnd,
                AirTemperature = dive.AirTemperature,
                WaterTemperature = dive.WaterTemperature,
                Visibility = dive.Visibility,
                TotalMinutes = dive.TotalMinutes,
                MaxDepth = dive.MaxDepth,
                FiveMetersMinutes = dive.FiveMetersMinutes,
                SuitType = dive.SuitType,
                Latitude = dive.DiveX,
                Longitude = dive.DiveY,
                Comments = dive.Comments
            };
        }

    }

    public class PhotoDetails
    {
        public string Comment;
        public DateTime Date;
        public int Size;
    }

    public class ShortDiveModel
    {       
        public decimal DiveID { get; set; }
        public bool Selected { get { return selected; } set { selected = value; } }
        private bool selected = false;
        public decimal DiveNumber { get; set; }
        public DateTime DiveDate { get; set; }       
        private DateTime _diveDate = DateTime.Now;
        public double? Depth { get; set; }
        public byte? Time { get; set; }
        public Nullable<decimal> CountryID { get; set; }
        public string CountryName { 
            get 
            {
                throw new NotImplementedException();
            }
        }       
    }   
}