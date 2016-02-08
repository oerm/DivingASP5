using AutoMapper;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers
{
    public class AuthController : Controller
    {       
        const int emptyCountryCode = 0;

        //TODO - Add to resource file
        const string changedOkText = "Changes in profile saved successfuly";

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
        public async Task<IActionResult> Register(NewUserViewModel viewModel)
        {
            try
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message.ToString());
            }
            finally
            {
                FillViewBag();              
            }
            return View(viewModel);

        }

        public async Task<IActionResult> Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _manager.FindByEmailAsync(User.Identity.Name);                    
                var viewModel = Mapper.Map<EditUserViewModel>(user);

                //TODO - Find out why automapper dont work
                viewModel.BirthDay = user.Birth;
                viewModel.Phone = user.PhoneNumber;
                viewModel.CountryKod = user.DicCountryId;

                if (!viewModel.CountryKod.HasValue || viewModel.CountryKod == emptyCountryCode) FillViewBag();
                else FillViewBag(viewModel.CountryKod.Value);

                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserInfo = await _manager.FindByEmailAsync(User.Identity.Name);
                

                User updatedUserInfo = Mapper.Map<User>(viewModel);

                //TODO - Find out why automapper dont work
                updatedUserInfo.Birth = viewModel.BirthDay;
                updatedUserInfo.PhoneNumber = viewModel.Phone;

                if (viewModel.NewPassword != null)
                {
                    if (!string.Equals(viewModel.NewPassword, viewModel.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Passwords mismatch");
                    }
                    else if (ModelState.IsValid)
                    {
                        await _manager.ChangePasswordAsync(currentUserInfo, viewModel.OldPassword, viewModel.NewPassword);
                    }
                }

                currentUserInfo.Email = updatedUserInfo.Email;
                currentUserInfo.Adress = updatedUserInfo.Adress;
                currentUserInfo.Birth = updatedUserInfo.Birth;
                currentUserInfo.City = updatedUserInfo.City;
                currentUserInfo.DicCountry = viewModel.CountryKod != 0 ? _context.DicCountries.Where(c => c.CountryKod == viewModel.CountryKod).First() : null;
                currentUserInfo.PhoneNumber = updatedUserInfo.PhoneNumber;
                if (updatedUserInfo.Photo != null) currentUserInfo.Photo = updatedUserInfo.Photo;

                var result = await _manager.UpdateAsync(currentUserInfo);
                if (result.Succeeded)
                {
                    ViewBag.ChangedOkText = changedOkText;
                    if (viewModel.CountryKod == emptyCountryCode) FillViewBag();
                    else FillViewBag(viewModel.CountryKod.Value);
                }

                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        private void FillViewBag(int selectedCountryCode = emptyCountryCode)
        {
            var countries = _context.DicCountries.Select(item =>
                                                         new SelectListItem()
                                                         {
                                                             Text = item.ValueEU,
                                                             Value = item.CountryKod.ToString(),
                                                             Selected = string.Equals(item.CountryKod, AuthController.emptyCountryCode)
                                                         })
                                                 .OrderBy(item=> item.Text)
                                                 .ToList();

            if (selectedCountryCode != AuthController.emptyCountryCode)
            {
                var selectedCountry = countries.Where(c => c.Value.Equals(selectedCountryCode))
                                               .FirstOrDefault();

                if (selectedCountry != null) selectedCountry.Selected = true;
            }
            ViewBag.Countries = countries;
        }
    }
}
