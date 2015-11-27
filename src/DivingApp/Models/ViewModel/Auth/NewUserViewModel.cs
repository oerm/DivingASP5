using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel.Auth
{
    public class NewUserViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "DateBirth should be in Datatime format")]
        public DateTime BirthDay { get; set; }

        public int CountryKod { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Upload)]
        public byte[] ImageUpload { get; set; }

    }

}
