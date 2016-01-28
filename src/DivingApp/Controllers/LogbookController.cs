using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using DivingApp.Models.DataModel;
using DivingApp.Models;
using Microsoft.AspNet.Mvc.Rendering;

namespace DivingApp.Controllers
{

    public class LogbookController : Controller
    {
        const int emptyCountryCode = 0;

        const string LogoutControllerName = "Home";
        const string LogoutDefaultActionName = "Index";

        EntityContext _context;
        SignInManager<User> _signManager;

        public LogbookController(SignInManager<User> signManager, EntityContext context)
        {
            _signManager = signManager;
            _context = context;
        }

        public IActionResult Dives()
        {
            if (User.Identity.IsAuthenticated)
            {
                FillViewBag();
                return View();
            }
            return RedirectToAction(LogbookController.LogoutDefaultActionName, LogbookController.LogoutControllerName);
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signManager.SignOutAsync();
            }   
            return RedirectToAction(LogbookController.LogoutDefaultActionName, LogbookController.LogoutControllerName);
        }

        private void FillViewBag()
        {
            var countries = _context.DicCountries.Select(item =>
                                                         new SelectListItem()
                                                         {
                                                             Text = item.ValueEU,
                                                             Value = item.CountryKod.ToString()
                                                         })
                                                 .OrderBy(item => item.Text)
                                                 .ToList();

            countries.Add(new SelectListItem()
            {
                Text = "Not selected",
                Value = LogbookController.emptyCountryCode.ToString(),
                Selected = true
            });


            var tanks = _context.DicTankTypes.ToArray().Select((item,i) =>
                                                        new SelectListItem()
                                                        {
                                                            Selected = i==0,
                                                            Text = item.TankValue,
                                                            Value = item.TankId.ToString()
                                                        })
                                                .OrderBy(item => item.Text)
                                                .ToList();

            var weights = _context.DicWeightOk.ToArray().Select((item, i) =>
                                                       new SelectListItem()
                                                       {
                                                           Selected = i == 0,
                                                           Text = item.WeightOkValue,
                                                           Value = item.WeightOkID.ToString()
                                                       })
                                               .OrderBy(item => item.Text)
                                               .ToList();

            var suits = _context.DicSuitTypes.ToArray().Select((item, i) =>
                                                    new SelectListItem()
                                                    {
                                                        Selected = i == 0,
                                                        Text = item.SuitValue,
                                                        Value = item.SuitID.ToString()
                                                    })
                                            .OrderBy(item => item.Text)
                                            .ToList();


            ViewBag.Suits = suits;
            ViewBag.WeightOk = weights;
            ViewBag.Tanks = tanks;
            ViewBag.Countries = countries;
        }
    }
}
