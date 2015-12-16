using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DivingApp.Models.ViewModel.Auth;
using DivingApp.Models;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Identity;
using DivingApp.Models.DataModel;
using AutoMapper;
using DivingApp.Models.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers
{
    public class AuthController : Controller
    {
        const string selectedCode = "804";

        EntityContext _context;
        UserManager<User> _manager;
        SignInManager<User> _signManager;

        public AuthController(EntityContext context, UserManager<User> userManager, SignInManager<User> signManager)
        {
            _context = context;
            _manager = userManager;
            _signManager = signManager;
        }
       
        public IActionResult Register()
        {
            FillViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel viewModel)
        {
            if (!string.Equals(viewModel.Password, viewModel.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords mismatch");
            }           

            if (ModelState.IsValid)
            {
                User model = Mapper.Map<User>(viewModel);
                var result = await _manager.CreateAsync(model, viewModel.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(model, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", string.Join(Environment.NewLine, result.Errors.Select(er => er.Description)));
                }
            }

            FillViewBag();
            return View(viewModel);
        }
        
        public IActionResult Edit()
        {
            if (User.Identity.IsAuthenticated)
            {   
               // var user =  User.Identity.Name              
              //  var viewModel = Mapper.Map<UserViewModel>(User);

              //  FillViewBag();
              //  return View(viewModel);
            }
            return RedirectToAction("Index", "Home");         
        }

        private void FillViewBag()
        {
            var countries = _context.DicCountries.Select(item => new SelectListItem()
            {
                Text = item.ValueEU,
                Value = item.CountryKod.ToString()
            }).ToList();

            var selectedCountry = countries.Where(c => c.Value.Equals(selectedCode)).FirstOrDefault();
            if (selectedCountry != null) selectedCountry.Selected = true;
            ViewBag.Countries = countries;
        }
    }
}
