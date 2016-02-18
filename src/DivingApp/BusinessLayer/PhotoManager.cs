using System;
using System.Collections.Generic;
using System.Linq;
using ImageProcessor;
using DivingApp.BusinessLayer.Interface;
using DivingApp.Models;
using Microsoft.Data.Entity;
using System.IO;
using ImageProcessor.Imaging.Formats;
using DivingApp.Models.DataModel;

namespace DivingApp.BusinessLayer
{
    public class PhotoManager : IPhotoManager
    {
        private const string notFoundString = "Photo with id {0} not found for this user";
        private const int qualityProcent = 20;
        private static object locker = new object();

        public PhotoManager()
        {
        }

        public byte[] GetFlag(int countryCode)
        {
            using (EntityContext _context = new EntityContext())
            {
                return _context.DicCountries.Where(c => c.CountryKod == countryCode)
                                            .Select(c => c.Flag)
                                            .First();
            }
        }      

        public IEnumerable<long> GetPhotoIdsByDiveId(string userId, long diveId, long minPhotoId = -1)
        {
            using (EntityContext _context = new EntityContext())
            {
                var dive = _context.Dives.Where(d => d.User.Id == userId && d.Status && d.DiveID == diveId)
                                 .Include(d => d.Photos)
                                 .First();

                return dive.Photos.OrderBy(p => p.PhotoID)
                                  .Where(p => p.PhotoID > minPhotoId && p.Status)
                                  .Select(p => p.PhotoID);
            }                                   
        }


        public byte[] GetPhoto(string userId, long photoId)
        {
            using (EntityContext _context = new EntityContext())
            {               
                try
                {
                    var diveId = _context.Photos.Where(p => p.PhotoID == photoId)
                                                          .Select(p => p.DiveID)
                                                          .First();

                    var isUserPhotoCheck = _context.Dives.Where(d => d.User.Id == userId && d.Status && d.DiveID == diveId);
                                               
                    if (isUserPhotoCheck.Any())
                    {
                        return _context.Photos.Where(p => p.PhotoID == photoId && p.Status)
                                                   .Select(p => p.PhotoVal)
                                                   .First();
                    }
                    else
                    {
                        throw new Exception(string.Format(notFoundString, photoId));
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

              
            }
        }

        public byte[] GetThumbPhoto(string userId, long photoId)
        {
            using (EntityContext _context = new EntityContext())
            {
                var diveId = _context.Photos.Where(p => p.PhotoID == photoId)
                                                        .Select(p => p.DiveID)
                                                        .First();

                var isUserPhotoCheck = _context.Dives.Where(d => d.User.Id == userId && d.Status && d.DiveID == diveId);

                if (isUserPhotoCheck.Any())
                {
                  
                        return GetThumb(_context.Photos.Where(p => p.PhotoID == photoId)
                                                   .Select(p => p.PhotoVal)
                                                   .First(), qualityProcent);                   
                }
                else
                {
                    throw new Exception(string.Format(notFoundString, photoId));
                }
            }
        }

        public long AddPhoto(long diveId, User user, byte[] photo, string name)
        {
            using (EntityContext _context = new EntityContext())
            {
                var dive = _context.Dives.Where(d => d.User.Id == user.Id && d.DiveID == diveId).First();
                var photoDesc = new Photos()
                {
                    DiveID = diveId,
                    Status = true,
                    PhotoDate = DateTime.Now,
                    PhotoName = name,
                    PhotoVal = photo
                };

                _context.Photos.Add(photoDesc);
                _context.SaveChanges();


                return photoDesc.PhotoID;
            }
        }

        public Boolean DeletePhoto(long photoId, string userId)
        {
            using (EntityContext _context = new EntityContext())
            {
                var rowsDeleted = 0;
                var diveId = _context.Photos.Where(p => p.PhotoID == photoId)
                                                      .Select(p => p.DiveID)
                                                      .First();

                var isUserPhotoCheck = _context.Dives.Where(d => d.User.Id == userId && d.Status && d.DiveID == diveId);

                if (isUserPhotoCheck.Any())
                {
                    _context.Photos.Where(p => p.PhotoID == photoId).First().Status = false;
                    rowsDeleted = _context.SaveChanges();
                }

                return rowsDeleted > 0;
            }
        }

        private byte[] GetThumb(byte[] photo, int qualityProcentage)
        {
            lock (locker)
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

}