using DivingApp.BusinessLayer;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers
{
    public class PassportController : Controller
    {
        UserManager<User> _userManager;
        IPassportManager _passportManager;

        public PassportController(UserManager<User> userManager)
        {
            _userManager = userManager;
            _passportManager = new PassportManager(); 
        }

        [HttpGet("passport/external/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> External(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var passport = _passportManager.GetUserPassport(user);
            return View(passport);
        }
    }
}
