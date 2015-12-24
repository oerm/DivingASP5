using AutoMapper;
using DivingApp.Common.Convertor;
using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel;
using DivingApp.Models.ViewModel.Auth;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Mappings
{
    public sealed class CommonBootstrap
    {
        private const string NoDataTextMessage = "Unknown";
        public static CommonBootstrap Instance
        {
            get
            {
                
                return instance;
            }
        }

        private static readonly CommonBootstrap instance = new CommonBootstrap();        

        private CommonBootstrap()
        {
            InitializeCommonMappings();
        }

        private void InitializeCommonMappings()
        {
            Mapper.CreateMap<DateTime?, DateTime>()
               .ConvertUsing<NullableDateTimeConverter>();

            Mapper.CreateMap<NewUserViewModel, User>()
                .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email))
                .ForMember(g => g.Email, r => r.MapFrom(s => s.Email))
                .ForMember(g => g.FirstName, r => r.MapFrom(s => s.FirstName))
                .ForMember(g => g.LastName, r => r.MapFrom(s => s.LastName))
                .ForMember(g => g.DicCountryId, r => r.MapFrom(s => s.CountryKod))
                .ForMember(g => g.Birth, r => r.MapFrom(s => s.BirthDay))
                .ForMember(g => g.Adress, r => r.MapFrom(s => s.Adress))
                .ForMember(g => g.PhoneNumber, r => r.MapFrom(s => s.Phone))
                .ForMember(g => g.Photo, r => r.MapFrom(s => s.ImageUpload))
                .ForMember(g => g.City, r => r.MapFrom(s => s.City))
                .ReverseMap();

            Mapper.CreateMap<EditUserViewModel, User>()
               .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email))
               .ForMember(g => g.Email, r => r.MapFrom(s => s.Email))
               .ForMember(g => g.FirstName, r => r.MapFrom(s => s.FirstName))
               .ForMember(g => g.LastName, r => r.MapFrom(s => s.LastName))
               .ForMember(g => g.DicCountryId, r => r.MapFrom(s => s.CountryKod))
               .ForMember(g => g.Birth, r => r.MapFrom(s => s.BirthDay))
               .ForMember(g => g.Adress, r => r.MapFrom(s => s.Adress))
               .ForMember(g => g.PhoneNumber, r => r.MapFrom(s => s.Phone))
               .ForMember(g => g.Photo, r => r.MapFrom(s => s.ImageUpload))
               .ForMember(g => g.City, r => r.MapFrom(s => s.City))
               .ReverseMap();

            Mapper.CreateMap<EditUserViewModel, User>()
              .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email))
              .ForMember(g => g.Email, r => r.MapFrom(s => s.Email))
              .ForMember(g => g.FirstName, r => r.MapFrom(s => s.FirstName))
              .ForMember(g => g.LastName, r => r.MapFrom(s => s.LastName))
              .ForMember(g => g.DicCountryId, r => r.MapFrom(s => s.CountryKod))
              .ForMember(g => g.Birth, r => r.MapFrom(s => s.BirthDay))
              .ForMember(g => g.Adress, r => r.MapFrom(s => s.Adress))
              .ForMember(g => g.PhoneNumber, r => r.MapFrom(s => s.Phone))
              .ForMember(g => g.Photo, r => r.MapFrom(s => s.ImageUpload))
              .ForMember(g => g.City, r => r.MapFrom(s => s.City))
              .ReverseMap();

            Mapper.CreateMap<User, UsersSearchResultViewModel>()
              .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email))
              .ForMember(g => g.FullName, r => r.MapFrom(s => string.IsNullOrWhiteSpace((s.FirstName + " " + s.LastName))
                                                              ? NoDataTextMessage
                                                              : (s.FirstName + " " + s.LastName).Trim()))
              .ForMember(g => g.CountryKod, r => r.MapFrom(s => s.DicCountryId))             
              .ForMember(g => g.FullAddress, r => r.MapFrom(s => string.Join(" ",
                                                                             s.DicCountry.ValueEU,
                                                                             s.City, 
                                                                             s.Adress)))
              .ForMember(g => g.BirthYear, r => r.MapFrom(s => s.Birth.HasValue ? s.Birth.Value.Year.ToString() : NoDataTextMessage))
              .ReverseMap();        

            Mapper.CreateMap<NewUserViewModel, User>()
                .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email));

            Mapper.CreateMap<EditUserViewModel, User>()
               .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email));

            Mapper.CreateMap<IFormFile, byte[]>()
               .ConvertUsing<HttpPostedFileBaseTypeConverter>();
        }
    }
}
