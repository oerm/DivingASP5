using DivingApp.BusinessLayer;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;

namespace DivingApp.Controllers.Api
{
    public class DiveController: Controller
    {
        const string imageContentType = "image/jpeg";

        EntityContext _context;
        UserManager<User> _userManager;
        IPassportManager _passportManager;
        IPhotoManager _photoManager;
        IDiveManager _diveManager;

        public DiveController(EntityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _passportManager = new PassportManager(context);
            _photoManager = new PhotoManager(context);
            _diveManager = new DiveManager(context);
        }

        [AllowAnonymous]
        [Route("api/dives/getdiveswithcoordinates/{username}")]
        [HttpGet]
        public async Task<JsonResult> GetDivesWithCoordinates(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return Json(_passportManager.GetDivesGeoData(user));
        }

        [AllowAnonymous]
        [HttpGet("api/dives/getuserdivephotobyid/{Email}/{PhotoId}")]
        public async Task<IActionResult> GetUserDivePhotoById(string Email, long PhotoId)
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    try
                    {
                        var photo = _photoManager.GetPhoto(user.Id, PhotoId);
                        return File(photo, imageContentType);
                    }
                    catch
                    {
                        return new HttpNotFoundResult();
                    }
                }
            }
            return new HttpNotFoundResult();
        }

        [AllowAnonymous]
        [HttpGet("api/dives/getuserthumbdivephotobyid/{Email}/{PhotoId}")]
        public async Task<IActionResult> GetUserThumbDivePhotoById(string Email, long PhotoId)
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    try
                    {
                        var photo = _photoManager.GetThumbPhoto(user.Id, PhotoId);
                        return File(photo, imageContentType);
                    }
                    catch (Exception ex)
                    {
                        return new HttpNotFoundResult();
                    }
                }
            }
            return new HttpNotFoundResult();
        }

        [AllowAnonymous]
        [HttpGet("/api/dives/getdivedictionaries")]
        public IActionResult GetDictionaries()
        {
            var countries = _context.DicCountries.Select(item =>
                                                         new
                                                         {
                                                             Text = item.ValueEU,
                                                             Value = item.CountryKod
                                                         })
                                                 .OrderBy(item => item.Text)
                                                 .ToList();

            var tanks = _context.DicTankTypes.ToArray().Select((item, i) =>
                                                        new
                                                        {
                                                            Selected = i == 0,
                                                            Text = item.TankValue,
                                                            Value = item.TankId
                                                        })
                                                .OrderBy(item => item.Text)
                                                .ToList();

            var weights = _context.DicWeightOk.ToArray().Select((item, i) =>
                                                       new
                                                       {
                                                           Selected = i == 0,
                                                           Text = item.WeightOkValue,
                                                           Value = item.WeightOkID
                                                       })
                                               .OrderBy(item => item.Text)
                                               .ToList();

            var suits = _context.DicSuitTypes.ToArray().Select((item, i) =>
                                                    new
                                                    {
                                                        Selected = i == 0,
                                                        Text = item.SuitValue,
                                                        Value = item.SuitID
                                                    })
                                            .OrderBy(item => item.Text)
                                            .ToList();

            var time = _context.DicDiveTime.ToArray().Select((item, i) =>
                                                 new
                                                 {
                                                     Selected = i == 0,
                                                     Text = item.TimeValue,
                                                     Value = item.TimeId
                                                 })
                                         .OrderBy(item => item.Value)
                                         .ToList();

            return Json(new
            {
                Suits = suits,
                Weights = weights,
                Tanks = tanks,
                Countries = countries,
                Time = time
            });
        }

      

        [AllowAnonymous]
        [Route("api/dives/getuserphotoidsbydiveids/{userMail}/{diveId}/{minId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserPhotoIdsByDiveIds(string userMail, long diveId, long minId)
        {
            if (!string.IsNullOrWhiteSpace(userMail))
            {
                var user = await _userManager.FindByEmailAsync(userMail);
                if (user != null)
                {
                    try
                    {
                        var photos = _photoManager.GetPhotoIdsByDiveId(user.Id, diveId, minId);
                        return Json(photos);
                    }
                    catch
                    {
                        return new HttpNotFoundResult();
                    }
                }
            }
            return new HttpNotFoundResult();        
        }

        //TODO - make proper caching
        [Route("api/dives/getuserdives/{nocache}")]
        [HttpGet]
        public async Task<IActionResult> GetShortDives()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var dives = _diveManager.GetShortDivesListByUserId(user.Id);
                return Json(dives);
            }
            return new HttpUnauthorizedResult();        
        }

        //TODO - make proper caching
        [Route("api/dives/getuserdivebyid/{diveId}/{nocache}")]
        [HttpGet]
        public async Task<IActionResult> GetDiveById(long diveId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var dive = _diveManager.GetDiveById(user.Id,diveId);
                return Json(dive);
            }
            return new HttpUnauthorizedResult();
        }

        [Route("api/dives/saveDive")]
        [HttpPost]
        public async Task<IActionResult> SaveDive(DiveViewModel dive)
        {            
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                   
                    if (dive.DiveID == 0)
                    {
                        _diveManager.SaveDive(dive, user);
                    }
                    else
                    {
                        _diveManager.UpdateDive(dive, user);
                    }
                     
                    return new HttpOkResult();
                }
                this.Response.StatusCode = 400;
                return Json(ModelState.Values.Where(v => v.ValidationState == Microsoft.AspNet.Mvc.ModelBinding.ModelValidationState.Invalid)
                                             .Select(m => new
                                             {
                                                 error = m.Errors.First().ErrorMessage.ToString()
                                             }));
            }
            return new HttpUnauthorizedResult();
        }

        [Route("api/dives/updateDive")]
        [HttpPut]
        public async Task<IActionResult> UpdateDive(DiveViewModel dive)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                    if (dive.DiveID == 0)
                    {
                        var result = _diveManager.UpdateDive(dive, user);
                    }
                    return new HttpOkResult();
                }
                this.Response.StatusCode = 400;
                return Json(ModelState.Values.Where(v => v.ValidationState == Microsoft.AspNet.Mvc.ModelBinding.ModelValidationState.Invalid)
                                             .Select(m => new
                                             {
                                                 error = m.Errors.First().ErrorMessage.ToString()
                                             }));
            }
            return new HttpUnauthorizedResult();
        }

        [Route("api/dives/deleteDive/{diveId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDive(long diveId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                    if (diveId > 0)
                    {
                        var result = _diveManager.DeleteDive(diveId, user);
                    }
                    return new HttpOkResult();
                }
                return new BadRequestResult();             
            }
            return new HttpUnauthorizedResult();
        }
    }
}
