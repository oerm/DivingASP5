using AutoMapper;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Controllers.Api
{
    public class UserController : Controller
    {
        const string imageContentType = "image/jpeg";

        EntityContext _context;
        UserManager<User> _userManager;
        IPhotoManager _photoManager;

        public UserController(EntityContext context, UserManager<User> userManager, IPhotoManager photoManager)
        {
            _context = context;
            _userManager = userManager;
            _photoManager = photoManager;
        }

        [HttpGet("api/users/getusersbyname/{name}")]
        [AllowAnonymous]
        public JsonResult GetUsersByName(string name)
        {
            var foundUsers = _context.Users
#if !DEBUG
                                 .Where(u => u.FirstName.StartsWith(name) || u.LastName.StartsWith(name))
#endif
                                 .ToList();

            var searchResults = Mapper.Map<IEnumerable<UsersSearchResultViewModel>>(foundUsers);

            return Json(searchResults);
        }  

        [HttpGet("api/getcountryflag/{countrycode}")]
        [AllowAnonymous]
        public ActionResult GetFlag(int countrycode)
        {            
            return base.File(_photoManager.GetFlag(countrycode), "image/jpeg");
        }

        [HttpGet("api/getuserphoto/{Email}")]
        public async Task<IActionResult> GetPhotoByEmail(string Email)
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null && user.Photo != null && user.Photo.Length > 0) return File(user.Photo, UserController.imageContentType);
            }
            return new HttpNotFoundResult();
        }
    }
}
