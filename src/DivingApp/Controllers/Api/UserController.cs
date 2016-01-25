using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DivingApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using DivingApp.Models.DataModel;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using AutoMapper;
using DivingApp.Models.ViewModel;
using DivingApp.BusinessLayer.Interface;
using DivingApp.BusinessLayer;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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


        [HttpPost]
        [AllowAnonymous]
        public JsonResult SaveMainPhoto()
        {
            return null;
            //try
            //{
               
            //    var ext = Request.B .FileName.Split('\\')[Request.Files[0].FileName.Split('\\').Length - 1];
            //    var filename = ControllerContext.HttpContext.Server.MapPath("~/Uploads//" + Request.Form["guid"] + "_" + ext);
            //    Request.Files[0].SaveAs(filename);
            //    if (Request.Params["userId"] != "")
            //    {
            //        var root = new DIVINGEntities();
            //        var userToChange = root.Users.Where(usr => usr.Email == Request.Params["userId"].ToString()).FirstOrDefault();
            //        byte[] buffer = new byte[Request.Files[0].InputStream.Length];
            //        Request.Files[0].InputStream.Read(buffer, 0, (int)Request.Files[0].InputStream.Length);
            //        userToChange.Photo = buffer;
            //        root.SaveChanges();
            //    }
            //}
            //catch {
            //    return Content("failed");
            //}
            //return Content("success");
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
