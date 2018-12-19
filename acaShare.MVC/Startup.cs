using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.EFPersistence;
using acaShare.MVC.Common;
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
using Microsoft.Extensions.FileProviders;

namespace acaShare.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMainPanelService, MainPanelService>();
            services.AddScoped<IUniversityTreeManagementService, UniversityTreeManagementService>();
            services.AddScoped<IUniversityTreeTraversalService, UniversityTreeTraversalService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesManagementService, RolesManagementService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<ISidebarService, SidebarService>();
            services.AddSingleton<IFormFilesManagement>(f => new FormFilesManagement(HostingEnvironment));
            services.AddSingleton<IFilesValidator, FilesValidator>();
            services.AddScoped<ValidateMaterial>();

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

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AcaShareDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

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
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseStaticFiles(); // For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), SharedResourcesLibrary.Properties.Resources.UploadsFolderName)),
                RequestPath = "/" + SharedResourcesLibrary.Properties.Resources.UploadsFolderName
            });

            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    ctx.Request.Path = "/error/404";
                    await next();
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area=Main}/{controller=List}/{action=AvailableUniversities}/{id?}");

                //routes.MapAreaRoute(
                //    name: "default",
                //    areaName: "Home",
                //    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
