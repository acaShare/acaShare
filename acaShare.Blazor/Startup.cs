using acaShare.Blazor.Areas.Identity;
using acaShare.Blazor.Data;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.EFPersistence;
using acaShare.ServiceLayer.Interfaces;
using acaShare.ServiceLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace acaShare.Blazor
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUniversityTreeTraversalService, UniversityTreeTraversalService>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesManagementService, RolesManagementService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<ISidebarService, SidebarService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMainModeratorService, MainModeratorService>();

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

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.Redirect("/Identity/WelcomePage/WelcomePage");
                        return Task.CompletedTask;
                    }
                };
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
