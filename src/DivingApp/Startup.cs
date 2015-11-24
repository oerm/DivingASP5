using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using DivingApp.Models;
using AutoMapper;
using DivingApp.Models.DataModel;
using Microsoft.AspNet.Identity.EntityFramework;

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

            // Mapper.Map()

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
