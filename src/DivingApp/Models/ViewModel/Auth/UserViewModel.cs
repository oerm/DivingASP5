using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel.Auth
{
    public class UserViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nullable<DateTime> BirthDay { get; set; }

        public int CountryKod { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageUpload { get; set; }

    }

}
