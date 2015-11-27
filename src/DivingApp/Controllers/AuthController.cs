using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DivingApp.Models.ViewModel.Auth;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers
{
    public class AuthController : Controller
    {
        // GET: /<controller>/
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(NewUserViewModel model)
        {
            if (!string.Equals(model.Password, model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords mismatch");
            }
            foreach (var field in ModelState.Where(f => f.Value.ValidationState == Microsoft.AspNet.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
               // ModelState.AddModelError("", string.Join("; ", field.Value.Errors.Select(er => er.ErrorMessage).ToArray()));
            }
         
            //if (ModelState.IsValid)
            //{
            //    RedirectToAction("index", "home");
            //}
            return View(model);

        }
    }
}
