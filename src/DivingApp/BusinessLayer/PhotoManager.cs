using System;
using System.Collections.Generic;
using System.Linq;
using ImageProcessor;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using Microsoft.Data.Entity;
using System.IO;
using ImageProcessor.Imaging.Formats;

namespace DivingApp.BusinessLayer
{
    public class PhotoManager : IPhotoManager
    {
        private const string notFoundString = "Photo with id {0} not found for this user";
        private const int qualityProcent = 20;
        EntityContext _context; 

        public PhotoManager(EntityContext context)
        {
            _context = context;
        }

        public byte[] GetFlag(int countryCode)
        {
            return _context.DicCountries.Where(c => c.CountryKod == countryCode)
                                        .Select(c => c.Flag)
                                        .First();
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

        public IEnumerable<long> GetPhotoIdsByDiveId(string userId, long diveId, long minPhotoId = -1)
        {
            var dive = _context.Dives.Where(d => d.User.Id == userId && d.Status && d.DiveID == diveId)
                                 .Include(d => d.Photos)
                                 .First();
                             
            return dive.Photos.OrderBy(p => p.PhotoID)
                              .Where(p => p.PhotoID > minPhotoId)
                              .Select(p => p.PhotoID);                                     
        }


        public byte[] GetPhoto(string userId, long photoId)
        {
            var isUserPhotoCheck = _context.Dives.Where(d => d.User.Id == userId && d.Status == true)
                                                 .Include(d => d.Photos)
                                                 .ToArray()
                                                 .Where(d => d.Photos.Any(p => p.PhotoID == photoId));

            if (isUserPhotoCheck.Any())
            {
                return _context.PhotoImgSet.Where(p => p.PhotoID == photoId)
                                           .Select(p => p.PhotoVal)
                                           .First();
            }
            else
            {
                throw new Exception(string.Format(notFoundString, photoId));
            }
        }

        public byte[] GetThumbPhoto(string userId, long photoId)
        {
            var isUserPhotoCheck = _context.Dives.Where(d => d.User.Id == userId && d.Status)
                                                .Include(d => d.Photos)                                             
                                                .ToArray()
                                                .Where(d => d.Photos.Any(p => p.PhotoID == photoId));

            if (isUserPhotoCheck.Any())
            {
                return GetThumb(_context.PhotoImgSet.Where(p => p.PhotoID == photoId)
                                           .Select(p => p.PhotoVal)
                                           .First(), qualityProcent);
            }
            else
            {
                throw new Exception(string.Format(notFoundString, photoId));
            }
        }

        //    public PhotoDetails GetPhotoDetails(Decimal photoId)
        //    {
        //        var file = (from img in root.Photos where img.PhotoID == photoId select new { img.PhotoImg.PhotoVal, img.PhotoComment, img.PhotoDate }).First();
        //        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(file.PhotoVal);
        //        System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
        //        return new PhotoDetails() { Comment = file.PhotoComment, Date = file.PhotoDate, Size = fullsizeImage.Size };   
        //    }

        private byte[] GetThumb(byte[] photo, int qualityProcentage)
        {           
            using (var imageFactory = new ImageFactory())
            {
                using (var newStream = new MemoryStream())
                {
                    imageFactory.Load(photo)
                            .Resolution(7, 5)
                            .Format(new JpegFormat()
                            {
                                Quality = qualityProcentage
                            })
                            .Quality(qualityProcentage)                            
                            .Save(newStream);
                    byte[] modifiedFile = new byte[newStream.Length];
                    newStream.Read(modifiedFile, 0, modifiedFile.Length);
                    return modifiedFile;
                }                
            } 
        }
    }

}