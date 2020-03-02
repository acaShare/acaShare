using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.EFPersistence;
using acaShare.ServiceLayer.Interfaces;
using acaShare.ServiceLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace acaShare.WebAPI
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
            //services.Configure<AcaShareConfiguration>(Configuration.GetSection("AcaShareConfiguration"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMainPanelService, MainPanelService>();
            services.AddScoped<IUniversityTreeManagementService, UniversityTreeManagementService>();
            services.AddCors();

            services.AddDbContext<AcaShareDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration["AcaShareConfiguration:ConnectionString"]);
            });
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
        }
    }
}
