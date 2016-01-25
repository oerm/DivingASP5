using DivingApp.Models.ViewModel;
using System.Collections.Generic;

namespace DivingApp.BusinessLayer.Interface
{
    public interface IDiveManager
    {
        IEnumerable<DiveShortViewModel> GetShortDivesListByUserId(string userId);
    }
}