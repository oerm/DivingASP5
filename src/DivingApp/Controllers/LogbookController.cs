using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using DivingApp.Models.DataModel;

namespace DivingApp.Controllers
{

    [Authorize]
    public class LogbookController : Controller
    {
        const string LogoutControllerName = "Home";
        const string LogoutDefaultActionName = "Index";

        SignInManager<User> _signManager;

        public LogbookController(SignInManager<User> signManager)
        {
            _signManager = signManager;
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signManager.SignOutAsync();
            }   
            return RedirectToAction(LogbookController.LogoutDefaultActionName, LogbookController.LogoutControllerName);
        }
    }
}
