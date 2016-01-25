using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivingApp.Models.ViewModel;
using Microsoft.Data.Entity;

namespace DivingApp.BusinessLayer
{
    public class DiveManager: IDiveManager
    {
        private EntityContext _context;

        public DiveManager(EntityContext context)
        {
            _context = context;
        }

        public IEnumerable<DiveShortViewModel> GetShortDivesListByUserId(string userId)
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
                                          DiveDate = d.DiveDate.ToShortDateString(),
                                          Depth = d.MaxDepth,
                                          Time = d.TotalMinutes
                                      }).Reverse();
            return dives;
           
        }
    }
}
