using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel;
using System.Collections.Generic;

namespace DivingApp.BusinessLayer.Interface
{
    public interface IPassportManager
    {
        PassportViewModel GetUserPassport(User user);

        IEnumerable<DiveGeoViewModel> GetDivesGeoData(User user);
    }
}