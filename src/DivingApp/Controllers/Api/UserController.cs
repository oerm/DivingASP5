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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers.Api
{
    public class UserController : Controller
    {
        const string imageContentType = "image/jpeg";

        EntityContext _context;
        UserManager<User> _manager;

        public UserController(EntityContext context, UserManager<User> manager)
        {
            _context = context;
            _manager = manager;
        }

        [HttpGet("api/users/getusersbyname/{name}")]
        public JsonResult GetUsersByName(string name)
        {
            try
            {
                var users = _context.Dives.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            var projection = (from usr in _context.Users
                              where (usr.FirstName + " " + usr.LastName).StartsWith(name)
                              select new
                              {
                                  id = usr.Id,
                                  email = usr.Email,
                                  fullName = usr.FirstName + " " + usr.LastName,
                                  age = usr.Birth.Value,
                               //   country = usr.Dic_Countries.dic_val_ru,
                               //   countryCode = usr.Dic_Countries.dic_val_kod,
                                  city = usr.City,
                                  address = usr.Adress
                              }).ToList();
            return Json(projection);
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

        [HttpGet("api/getuserphoto/{Email}")]
        public async Task<IActionResult> GetPhotoByEmail(string Email)
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var user = await _manager.FindByEmailAsync(Email);              
                if (user != null && user.Photo != null && user.Photo.Length > 0) return File(user.Photo, UserController.imageContentType);
            }
            return new HttpNotFoundResult();
        }

    }
}
