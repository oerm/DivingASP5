using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivingApp.Models.ViewModel;
using Microsoft.Data.Entity;
using DivingApp.Models.DataModel;

namespace DivingApp.BusinessLayer
{
    public class DiveManager : IDiveManager
    {
        private EntityContext _context;

        public DiveManager(EntityContext context)
        {
            _context = context;
        }

        public DiveViewModel GetDiveById(string userId, long diveId)
        {
            var dive = _context.Dives.Where(d => d.User.Id == userId && d.DiveID == diveId && d.Status)
                                    .Include(d => d.Countries)
                                    .Include(d => d.Photos)
                                    .ToArray()
                                    .Select(d => new DiveViewModel
                                    {
                                        DiveID = d.DiveID,
                                        CountryId = d.Country,
                                        AirTemperature = d.AirTemperature,
                                        Comments = d.Comments,
                                        DiveDateString = d.DiveDate.ToString("dd/MM/yyyy"),
                                        DiveTime = d.DiveTime,
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
                                        Photos = d.Photos.Select(p => new PhotoViewModel
                                        {
                                            PhotoId = (int)p.PhotoID,
                                            Date = p.PhotoDate,
                                            Comment = p.PhotoComment
                                        }).ToArray()
                                    }).First();


            return dive;
        }

        public IEnumerable<DiveShortViewModel> GetShortDivesListByUserId(string userId)
        {
            try
            {
                var dives = _context.Dives.Where(d => d.User.Id == userId && d.Status)
                                          .Include(d => d.Countries)
                                          .OrderBy(d => d.DiveDate)
                                          .ToArray()
                                          .Select((d, n) => new DiveShortViewModel
                                          {
                                              DiveID = d.DiveID,
                                              DiveNumber = n + 1,
                                              CountryID = d.Countries.CountryKod,
                                              CountryName = d.Countries.ValueEU,
                                              DiveDate = d.DiveDate.ToString("dd/MM/yyyy"),
                                              Depth = d.MaxDepth,
                                              Time = d.TotalMinutes
                                          }).Reverse();
                return dives;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveDive(DiveViewModel dive, User user)
        {
            try
            {
                var newDive = new Dive()
                {
                    User = user,
                    Country = dive.CountryId,
                    Countries = _context.DicCountries.Where(c => c.CountryKod == dive.CountryId).First(),
                    AirTemperature = dive.AirTemperature,
                    Comments = dive.Comments,
                    DiveDate = dive.DiveDate,
                    DiveTime = dive.DiveTime,
                    FiveMetersMinutes = dive.FiveMetersMinutes,
                    DiveX = dive.Latitude,
                    Location = dive.Location,
                    DiveY = dive.Longitude,
                    MaxDepth = dive.MaxDepth,
                    SuitType = dive.SuitType,
                    Tank = dive.Tank,
                    TankEnd = dive.TankEnd,
                    TankStart = dive.TankStart,
                    TotalMinutes = dive.TotalMinutes,
                    Visibility = dive.Visibility,
                    WaterTemperature = dive.WaterTemperature,
                    Weight = dive.Weight,
                    WeightIsOk = dive.WeightIsOk,
                    Status = true
                };
                _context.Dives.Add(newDive);

                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDive(long diveId, User user)
        {
            var dive = _context.Dives.Where(d => d.User.Id == user.Id && d.DiveID == diveId).First();
            _context.Remove(dive);
            return _context.SaveChanges() > 0;
        }
    }
}
