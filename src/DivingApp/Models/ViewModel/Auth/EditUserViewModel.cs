using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel.Auth
{
    public class EditUserViewModel: UserViewModelBase
    {
        [DataType(DataType.EmailAddress)]       
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]     
        public string ConfirmPassword { get; set; }
     
    }

}
