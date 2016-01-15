using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DivingApp.Models;
using Microsoft.AspNet.Identity;
using DivingApp.Models.DataModel;
using DivingApp.BusinessLayer.Interface;
using DivingSite.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers
{
    public class PassportController : Controller
    {
        EntityContext _context;
        UserManager<User> _userManager;
        IPassportManager _passportManager;

        public PassportController(EntityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _passportManager = new PassportManager(_context); 
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
