using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;

namespace DivingApp.BusinessLayer
{
    public class PhotoManager : IPhotoManager
    {
        EntityContext _context; 

        public PhotoManager(EntityContext context)
        {
            _context = context;
        }

        public byte[] GetFlag(int countryCode)
        {
            return (from img 
                    in _context.DicCountries
                    where img.CountryKod == countryCode
                    select img.Flag).First();
        }

    //    public decimal SaveImage(byte[] file, string filename, Decimal diveId)
    //    {
    //        Photos photo = new Photos()
    //        {
    //            DiveID = diveId,
    //            PhotoDate = DateTime.Now,
    //            PhotoName = filename,                

    //            Status = false
    //        };    
    //        photo.PhotoImg.PhotoVal=file;
    //        root.Photos.Add(photo);
    //        var rowsAdded = root.SaveChanges();
    //        return photo.PhotoID;
    //    }

    //    public bool ConfirmSaving(Decimal? photoId, String comment = "")
    //    {           
    //        var item = (from photo in root.Photos where photo.PhotoID == photoId select photo).FirstOrDefault();
    //        if (item != null)
    //        {
    //            item.PhotoComment = comment;
    //            item.Status = true;
    //            var rowsAdded = root.SaveChanges();
    //            return rowsAdded > 0;
    //        }
    //        return false;
    //    }

    //    public Boolean DeletePhoto(Decimal photoId)
    //    {
    //        var photo = (from img in root.Photos where img.PhotoID == photoId select img).First();
    //        photo.Status = false;
    //        var rowsDeleted = root.SaveChanges();
    //        return rowsDeleted > 0;
    //    }

    //    public string GetPhotoIdsInJSon(Decimal diveId, Decimal maxPhotoId=-1)
    //    {
    //        var photos = (from img in root.Photos where img.DiveID == diveId && img.DiveID > maxPhotoId && img.Status==true select img.PhotoID).ToList();
    //        return String.Join(",", photos);            
    //    }

    //    public byte[] GetPhoto(Decimal photoId)
    //    {     
    //        return (from img in root.Photos where img.PhotoID == photoId select img.PhotoImg.PhotoVal).First();
    //    }

    //    public byte[] GetThumbPhoto(Decimal photoId)
    //    {
    //        var bt = (from img in root.Photos where img.PhotoID == photoId select img.PhotoImg.PhotoVal).First();
    //        var thumb = GetThumb(bt, 120);
    //        return thumb;
    //    }

    //    public byte[] GetSmallThumbPhoto(Decimal photoId)
    //    {
    //        var bt = (from img in root.Photos where img.PhotoID == photoId select img.PhotoImg.PhotoVal).First();
    //        var thumb = GetThumb(bt, 52);
    //        return thumb;
    //    }

      

    //    public PhotoDetails GetPhotoDetails(Decimal photoId)
    //    {
    //        var file = (from img in root.Photos where img.PhotoID == photoId select new { img.PhotoImg.PhotoVal, img.PhotoComment, img.PhotoDate }).First();
    //        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(file.PhotoVal);
    //        System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
    //        return new PhotoDetails() { Comment = file.PhotoComment, Date = file.PhotoDate, Size = fullsizeImage.Size };   
    //    }

    //    private byte[] GetThumb(byte[] photo, int maxPixels)
    //    {
    //        if (photo != null && photo.Length > 0)
    //        {
    //            using (var stream = new MemoryStream(photo))
    //            {
    //                var img = Image.FromStream(stream);                 
    //                int originalWidth = img.Width;
    //                int originalHeight = img.Height;
    //                double factor = ((originalWidth > originalHeight ? ((double)maxPixels / originalWidth) : ((double)maxPixels / originalHeight)));
    //                var size = new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
    //                var thumb = img.GetThumbnailImage(size.Width, size.Height, null, new IntPtr());
    //                using (var thumbStream = new MemoryStream())
    //                {
    //                    thumb.Save(thumbStream, System.Drawing.Imaging.ImageFormat.Png);
    //                    return thumbStream.ToArray();
    //                }
    //            }
    //        }
    //        else return null;
    //    }     
    }

}