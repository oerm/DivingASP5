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
using Microsoft.AspNet.Http;
using System.IO;

namespace DivingApp.Controllers.Api
{
    public class DiveController: Controller
    {
        const string imageContentType = "image/jpeg";

        UserManager<User> _userManager;
        IPassportManager _passportManager;
        IPhotoManager _photoManager;
        IDiveManager _diveManager;

        public DiveController(UserManager<User> userManager)
        {
            _userManager = userManager;
            _passportManager = new PassportManager();
            _photoManager = new PhotoManager();
            _diveManager = new DiveManager();
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
                       return await this.GetUserDivePhotoById(Email, PhotoId);                    
                    }
                }
            }
            return new HttpNotFoundResult();
        }

        [AllowAnonymous]
        [HttpGet("/api/dives/getdivedictionaries")]
        public IActionResult GetDictionaries()
        {
            using (EntityContext _context = new EntityContext())
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

        [Route("api/dives/uploadfile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var diveId = int.Parse(this.Request.Form["diveid"]);
                var fileBase = (IFormFile)this.Request.Form.Files[0];
                MemoryStream target = new MemoryStream();
                fileBase.OpenReadStream().CopyTo(target);
                if (diveId > 0)
                {
                    var result = _photoManager.AddPhoto(diveId, user, target.ToArray(), fileBase.ContentDisposition);
                    var jsonResult = Json(
                        new
                        {
                            files = new PhotoResult[]
                            {
                                new PhotoResult
                                {
                                    url = "url",
                                    thumbnailUrl = string.Format("/api/dives/getuserthumbdivephotobyid/{0}/{1}", User.Identity.Name ,result),
                                    type = "image/jpeg",
                                    size = target.Length,
                                    deleteUrl = "deleteUrl",
                                    deleteType = "DELETE",
                                    id = result.ToString()
                                }
                            }
                        });
                    return jsonResult;
                }
              
                return new BadRequestResult();
            }
            return new HttpUnauthorizedResult();
        }
    }

   
}
