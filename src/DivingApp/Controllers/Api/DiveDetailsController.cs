﻿using DivingApp.BusinessLayer;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Threading.Tasks;

namespace DivingApp.Controllers.Api
{
    public class DiveDetailsController: Controller
    {
        const string imageContentType = "image/jpeg";

        EntityContext _context;
        UserManager<User> _userManager;
        IPassportManager _passportManager;
        IPhotoManager _photoManager;
        IDiveManager _diveManager;


        public DiveDetailsController(EntityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _passportManager = new PassportManager(context);
            _photoManager = new PhotoManager(context);
            _diveManager = new DiveManager(context);
        }

        [AllowAnonymous]
        [Route("api/getdiveswithcoordinates/{username}")]
        [HttpGet]
        public async Task<JsonResult> GetDivesWithCoordinates(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return Json(_passportManager.GetDivesGeoData(user));
        }

        [AllowAnonymous]
        [HttpGet("api/getuserdivephotobyid/{Email}/{PhotoId}")]
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
                    catch (Exception ex)
                    {
                        return new HttpNotFoundResult();
                    }
                }
            }
            return new HttpNotFoundResult();
        }

        [AllowAnonymous]
        [Route("api/getuserphotoidsbydiveids/{userMail}/{diveId}/{minId}")]
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
                    catch (Exception ex)
                    {
                        return new HttpNotFoundResult();
                    }
                }
            }
            return new HttpNotFoundResult();        
        }

     
        [Route("api/getuserdives")]
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

    }
}
