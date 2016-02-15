using DivingApp.Models.DataModel;
using System.Collections.Generic;

namespace DivingApp.BusinessLayer.Interface
{
    public interface IPhotoManager
    {
        byte[] GetFlag(int countryCode);

        byte[] GetPhoto(string userEmail, long photoId);

        byte[] GetThumbPhoto(string userEmail, long photoId);
      
        IEnumerable<long> GetPhotoIdsByDiveId(string userId, long diveId, long minPhotoId = -1);

        bool DeletePhoto(long photoId, string userId);

        long AddPhoto(long diveId, User user, byte[] photo, string name);
    }
}
