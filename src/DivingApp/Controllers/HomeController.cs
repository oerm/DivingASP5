using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DivingApp.Models.ViewModel;
using DivingApp.Models.DataModel;
using Microsoft.AspNet.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApp.Controllers
{
    public class HomeController : Controller
    {
        const string DiveControllerName = "Dives";
        const string DiveControllerDefaultActionName = "Logbook";

        //TODO - move to resource
        const string LoginFailedMessage = "Failed to login to the logbook with provided credetials";

        SignInManager<User> _signManager;
        UserManager<User> _userManager;

        public HomeController(SignInManager<User> signManager, UserManager<User> userManager)
        {
            _signManager = signManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(HomeController.DiveControllerName,
                                                                       HomeController.DiveControllerDefaultActionName);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                var signInResult = await _signManager.PasswordSignInAsync(user,
                                                                          viewModel.Password,
                                                                          viewModel.RememberMe,
                                                                          false);


                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction(HomeController.DiveControllerName,
                                                HomeController.DiveControllerDefaultActionName);
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                ModelState.AddModelError("", HomeController.LoginFailedMessage);
            }
            return View(viewModel);
        }

    }
}
