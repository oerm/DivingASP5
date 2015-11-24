using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DivingApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using DivingApp.Models.DataModel;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers.Api
{
    public class UserController : Controller
    {
        EntityContext _context;

        public UserController(EntityContext context)
        {
            this._context = context;
        }

        // GET: api/values
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
                                  id = usr.UserID,
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

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{

    }
}
