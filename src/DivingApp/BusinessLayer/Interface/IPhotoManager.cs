using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.BusinessLayer.Interface
{
    public interface IPhotoManager
    {
        byte[] GetFlag(int countryCode);

        byte[] GetPhoto(string userEmail, long photoId);

        byte[] GetThumbPhoto(string userEmail, long photoId);

        //bool ConfirmSaving(decimal? photoId, string comment = "");
        //bool DeletePhoto(decimal photoId);

        //byte[] GetPhoto(decimal photoId);
        //PhotoDetails GetPhotoDetails(decimal photoId);

        IEnumerable<long> GetPhotoIdsByDiveId(string userId, long diveId, long minPhotoId = -1);

       
        //byte[] GetSmallThumbPhoto(decimal photoId);
        //decimal SaveImage(byte[] file, string filename, decimal diveId);
    }
}
