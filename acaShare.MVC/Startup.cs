﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.EFPersistence;
using acaShare.ServiceLayer.Interfaces;
using acaShare.ServiceLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace acaShare.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMainPanelService, MainPanelService>();
            services.AddScoped<IUniversityTreeManagementService, UniversityTreeManagementService>();
            services.AddScoped<IUserService, UserService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AcaShareDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration["AcaShareConfiguration:ConnectionString"]);
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AcaShareDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area=Universities}/{controller=List}/{action=Index}/{id?}");

                //routes.MapAreaRoute(
                //    name: "default",
                //    areaName: "Home",
                //    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}