using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.BusinessLayer.Interface
{
    public interface IPhotoManager
    {
        byte[] GetFlag(int countryCode);

        //bool ConfirmSaving(decimal? photoId, string comment = "");
        //bool DeletePhoto(decimal photoId);

        //byte[] GetPhoto(decimal photoId);
        //PhotoDetails GetPhotoDetails(decimal photoId);
        //string GetPhotoIdsInJSon(decimal diveId, decimal maxPhotoId = -1);
        //byte[] GetThumbPhoto(decimal photoId);
        //byte[] GetSmallThumbPhoto(decimal photoId);
        //decimal SaveImage(byte[] file, string filename, decimal diveId);
    }
}
