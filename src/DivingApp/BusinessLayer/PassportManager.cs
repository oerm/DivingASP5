﻿using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel;
using System;
using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using DivingApp.Models.ViewModel.Helper;

namespace DivingSite.Models
{
    public class PassportManager : IPassportManager
    {
        private const string unknownUser = "Unknown";
        private EntityContext _context;

        public PassportManager(EntityContext context)
        {
            _context = context;
        }

        public PassportViewModel GetUserPassport(User user)
        {
            var model = new PassportViewModel();
            var dives = _context.Dives.Where(d => d.User.Id == user.Id && d.Status)
                                      .Include(d=> d.Countries)
                                      .Include(d=> d.Photos)
                                      .ToArray();
            model.Email = user.Email;
            model.Fio = string.Format("{0} {1}", user.FirstName, user.LastName);
            if (string.IsNullOrWhiteSpace(model.Fio)) model.Fio = unknownUser;
            DateTime today = DateTime.Today;
            int age = today.Year - user.Birth.Value.Year;
            if (user.Birth.Value > today.AddYears(-age)) age--;
            model.Age = age;
            var country = (from c in _context.DicCountries where c.CountryKod == user.DicCountryId select c).First();
            model.Country = country.ValueEU;
            model.CountryId = country.CountryKod;
            model.DivesCount = dives.Count();
            model.MaxDepth = dives.Select(d => d.MaxDepth.Value).Max();
            model.SumDiveMinutes = dives.Select(d => (int)d.TotalMinutes).Sum();
            model.diveCountries = dives.Where(d=> d.Countries!= null)
                                       .GroupBy(d => d.Countries.CountryKod)
                                       .Select(c=> new GroupedCountryViewModel
                                       {
                                           Count = c.Count(),
                                           Code = c.First().Countries.CountryKod,
                                           Name = c.First().Countries.ValueEU
                                       }).ToArray();


            model.Dives = dives.OrderBy(d => d.DiveDate)
                               .Select(d => new DiveViewModel
                               {
                                   DiveID = d.DiveID,
                                   CountryId = d.Country,
                                   AirTemperature = d.AirTemperature,
                                   Comments = d.Comments,
                                   DiveDate = d.DiveDate,
                                   DiveType = d.DiveType,
                                   FiveMetersMinutes = d.FiveMetersMinutes,
                                   Latitude = d.DiveX,
                                   Location = d.Location,
                                   Longitude = d.DiveY,
                                   MaxDepth = d.MaxDepth,
                                   SuitType = d.SuitType,
                                   Tank = d.Tank,
                                   TankEnd = d.TankEnd,
                                   TankStart = d.TankStart,
                                   TotalMinutes = d.TotalMinutes,
                                   Visibility = d.Visibility,
                                   WaterTemperature = d.WaterTemperature,
                                   Weight = d.Weight,
                                   WeightIsOk = d.WeightIsOk,
                                   Photos = d.Photos.Select(p=> new PhotoViewModel
                                   {
                                       PhotoId = (int)p.PhotoID,
                                       Date = p.PhotoDate,
                                       Comment = p.PhotoComment
                                   })
                               }).ToArray();


            model.Certs = _context.Certs.Where(cert => cert.User.Id == user.Id)
                                        .Select(c => new CertViewModel
                                        {
                                            CertID = c.DicCert.CertID,
                                            CertName = c.DicCert.CertName,
                                            CertNumber = c.CertNumber,
                                            DateArchieve = c.DateArchieve,
                                            Description = c.DicCert.Description,
                                            IsGeneral = c.DicCert.IsGeneral,
                                            Issuer = c.Issuer,
                                            Level = c.DicCert.Level,
                                            UserID = c.User.Id
                                        })
                                        .ToArray();

            return model;
        }

        public IEnumerable<DiveGeoViewModel> GetDivesGeoData(User user)
        {
            return _context.Dives.Where(d => d.User.Id == user.Id && d.Status)
                                 .Select(dive => new DiveGeoViewModel()
                                 {
                                     DiveId = dive.DiveID,
                                     DiveDate = dive.DiveDate,
                                     DiveComment = dive.Comments,
                                     Location = dive.Location,
                                     CoordinateX = dive.DiveX.ToString(),
                                     CoordinateY = dive.DiveY.ToString()
                                 });
        }
    }  
}