using AutoMapper;
using DivingApp.Common.Convertor;
using DivingApp.Common.Resolver;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using DivingApp.Models.ViewModel.Auth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Framework.DependencyInjection;
using System;

namespace DivingApp
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<EntityContext>();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;                              
            })
            .AddEntityFrameworkStores<EntityContext>();

            services.AddMvc();            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseIdentity();

            #region Automapper
            Mapper.CreateMap<DateTime?, DateTime>()
                .ConvertUsing<NullableDateTimeConverter>();

            Mapper.CreateMap<NewUserViewModel, User>()
                .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email))
                .ForMember(g => g.Email, r => r.MapFrom(s => s.Email))
                .ForMember(g => g.FirstName, r => r.MapFrom(s => s.FirstName))
                .ForMember(g => g.LastName, r => r.MapFrom(s => s.LastName))               
                .ForMember(g => g.DicCountryId, r => r.MapFrom(s => s.CountryKod))
                .ForMember(g => g.City, r => r.MapFrom(s => s.City))
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
               .ForMember(g => g.City, r => r.MapFrom(s => s.City))
               .ForMember(g => g.Adress, r => r.MapFrom(s => s.Adress))
               .ForMember(g => g.PhoneNumber, r => r.MapFrom(s => s.Phone))
               .ForMember(g => g.Photo, r => r.MapFrom(s => s.ImageUpload))
               .ForMember(g => g.City, r => r.MapFrom(s => s.City))
               .ReverseMap();

            Mapper.CreateMap<NewUserViewModel, User>()
                .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email));

            Mapper.CreateMap<EditUserViewModel, User>()
               .ForMember(g => g.UserName, r => r.MapFrom(s => s.Email));

            Mapper.CreateMap<IFormFile, byte[]>()
               .ConvertUsing<HttpPostedFileBaseTypeConverter>();
            #endregion

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });           
        }
    }
}
