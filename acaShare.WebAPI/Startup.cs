using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.EFPersistence;
using acaShare.ServiceLayer.Interfaces;
using acaShare.ServiceLayer.Services;
using acaShare.WebAPI.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace acaShare.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUniversityTreeTraversalService, UniversityTreeTraversalService>();
            services.AddScoped<IUniversityTreeManagementService, UniversityTreeManagementService>();
            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesManagementService, RolesManagementService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<ISidebarService, SidebarService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMainModeratorService, MainModeratorService>();
            services.AddSingleton<IFormFilesManagement>(f => new FormFilesManagement(WebHostEnvironment));

            services.AddDbContext<AcaShareDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                //options.UseSqlServer(Environment.GetEnvironmentVariable("SQLCONNSTR_ConnectionString"));
                options.UseSqlServer(Configuration["AcaShareConfiguration:ConnectionString"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AcaShareDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddApiAuthorization<IdentityUser, AcaShareDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
