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

        public DiveViewModel GetDiveById(string userId, long diveId)
        {
            using (EntityContext _context = new EntityContext())
            {
                DiveViewModel dive = null;
                try
                {
                    dive = _context.Dives.Where(d => d.User.Id == userId && d.DiveID == diveId && d.Status)
                                        .Include(d => d.Countries)
                                        .Include(d => d.Photos)                                       
                                        .Select(d => new DiveViewModel
                                        {
                                            DiveID = d.DiveID,
                                            CountryId = d.Country,
                                            AirTemperature = d.AirTemperature,
                                            Comments = d.Comments,                                       
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
                                            Photos = d.Photos.Where(p => p.Status).Select(p => new PhotoViewModel
                                            {
                                                PhotoId = p.PhotoID,
                                                Date = p.PhotoDate,
                                                Comment = p.PhotoComment
                                            }).ToArray()
                                        }).First();
                }
                catch(Exception ex)
                {

                }
                return dive;
            }
        }

        public IEnumerable<DiveShortViewModel> GetShortDivesListByUserId(string userId)
        {
            using (EntityContext _context = new EntityContext())
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
        }

        public bool SaveDive(DiveViewModel dive, User user)
        {
            using (EntityContext _context = new EntityContext())
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
                        UpdDate = DateTime.Now,
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
        }

        public bool UpdateDive(DiveViewModel dive, User user)
        {
            using (EntityContext _context = new EntityContext())
            {
                try
                {
                    var updDive = _context.Dives.Where(d => d.User.Id == user.Id && d.DiveID == dive.DiveID).First();
                    updDive.Country = dive.CountryId;
                    updDive.Countries = _context.DicCountries.Where(c => c.CountryKod == dive.CountryId).First();
                    updDive.AirTemperature = dive.AirTemperature;
                    updDive.Comments = dive.Comments;
                    updDive.DiveDate = dive.DiveDate;
                    updDive.DiveTime = dive.DiveTime;
                    updDive.FiveMetersMinutes = dive.FiveMetersMinutes;
                    updDive.DiveX = dive.Latitude;
                    updDive.Location = dive.Location;
                    updDive.DiveY = dive.Longitude;
                    updDive.MaxDepth = dive.MaxDepth;
                    updDive.SuitType = dive.SuitType;
                    updDive.Tank = dive.Tank;
                    updDive.TankEnd = dive.TankEnd;
                    updDive.TankStart = dive.TankStart;
                    updDive.TotalMinutes = dive.TotalMinutes;
                    updDive.Visibility = dive.Visibility;
                    updDive.WaterTemperature = dive.WaterTemperature;
                    updDive.Weight = dive.Weight;
                    updDive.WeightIsOk = dive.WeightIsOk;
                    updDive.UpdDate = DateTime.Now;
                    return _context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteDive(long diveId, User user)
        {
            using (EntityContext _context = new EntityContext())
            {
                var dive = _context.Dives.Where(d => d.User.Id == user.Id && d.DiveID == diveId).First();
                dive.Status = false;
                return _context.SaveChanges() > 0;
            }
        }      
    }
}
