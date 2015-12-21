using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel.Auth
{
    public abstract class UserViewModelBase
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nullable<DateTime> BirthDay { get; set; }

        public Nullable<int> CountryKod { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageUpload { get; set; }

    }

}
