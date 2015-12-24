using AutoMapper;
using DivingApp.Common.Convertor;
using DivingApp.Common.Resolver;
using DivingApp.Mappings;
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
          
            var doMappings = CommonBootstrap.Instance;           

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
