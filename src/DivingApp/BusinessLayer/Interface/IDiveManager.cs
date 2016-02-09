using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel;
using System.Collections.Generic;

namespace DivingApp.BusinessLayer.Interface
{
    public interface IDiveManager
    {
        IEnumerable<DiveShortViewModel> GetShortDivesListByUserId(string userId);

        DiveViewModel GetDiveById(string userId, long diveId);

        bool SaveDive(DiveViewModel dive, User user);

        bool DeleteDive(long diveId, User user);
    }
}