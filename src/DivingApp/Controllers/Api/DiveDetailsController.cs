using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using DivingSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Controllers.Api
{
    public class DiveDetailsController: Controller
    {
        EntityContext _context;
        UserManager<User> _userManager;
        IPassportManager _passportManager;

        public DiveDetailsController(EntityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _passportManager = new PassportManager(context);
        }

        [AllowAnonymous]
        [Route("api/GetDivesWithCoordinates/{username}")]
        [HttpGet]
        public async Task<JsonResult> GetDivesWithCoordinates(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return Json(_passportManager.GetDivesGeoData(user));
        }
    }
}
