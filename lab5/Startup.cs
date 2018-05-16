using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using IGILab1Norm;
using lab2.Middleware;
using lab2.Extensions.Filters;
using lab2.Models.AccountModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace lab2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<RentContext>();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddResponseCaching();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".Task.Session";
            });
            services.AddDbContext<AccountContext>(options =>
              options.UseSqlServer("Server=.\\SQLEXPRESS;Database=rentUserDB;Trusted_Connection=True;"));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AccountContext>();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(LoggerFilterAttribute));
                options.Filters.Add(typeof(ExceptionFilterAttribute));
                options.CacheProfiles.Add("Caching",
                    new CacheProfile()
                    {
                        Duration = 2 * 10 + 240,
                        Location = ResponseCacheLocation.Any
                    });
                options.CacheProfiles.Add("NoCaching",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            AccountInit.Initialize(serviceProvider).Wait();
            app.UseSession();
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=2*10+240");
                }
            });

            //app.UseOperatinCache("Cache elements");
      
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
