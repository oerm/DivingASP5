using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel
{
    public class UsersSearchResultViewModel
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string BirthYear { get; set; }

        public Nullable<int> CountryKod { get; set; }

        public string FullAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
