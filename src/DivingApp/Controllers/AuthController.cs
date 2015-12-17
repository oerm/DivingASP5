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
        const int emptyCountryCode = 0;

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

        public async Task<IActionResult> Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _manager.FindByEmailAsync(User.Identity.Name);
                var viewModel = Mapper.Map<UserViewModel>(user);

                //TODO - Find out why automapper dont work
                viewModel.BirthDay = user.Birth;
                viewModel.Phone = user.PhoneNumber;

                if (viewModel.CountryKod == AuthController.emptyCountryCode) FillViewBag();
                else FillViewBag(viewModel.CountryKod.ToString());

                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _manager.FindByEmailAsync(User.Identity.Name);
                var viewModel = Mapper.Map<UserViewModel>(user);

                //TODO - Find out why automapper dont work
                viewModel.BirthDay = user.Birth;
                viewModel.Phone = user.PhoneNumber;

                if (viewModel.CountryKod == AuthController.emptyCountryCode) FillViewBag();
                else FillViewBag(viewModel.CountryKod.ToString());

                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        private void FillViewBag(string selectedCountryCode = AuthController.selectedCode)
        {
            var countries = _context.DicCountries.Select(item =>
                                                         new SelectListItem()
                                                         {
                                                             Text = item.ValueEU,
                                                             Value = item.CountryKod.ToString()
                                                         })
                                                 .ToList();

            countries.Add(new SelectListItem()
            {
                Text = "Not selected",
                Value = AuthController.emptyCountryCode.ToString(),
                Selected = true
            });


            if (selectedCountryCode != AuthController.selectedCode)
            {
                var selectedCountry = countries.Where(c => c.Value.Equals(selectedCountryCode))
                                               .FirstOrDefault();

                if (selectedCountry != null) selectedCountry.Selected = true;
            }
            ViewBag.Countries = countries;
        }
    }
}
