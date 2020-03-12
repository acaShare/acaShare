using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(acaShare.WebAPI.Areas.Identity.IdentityHostingStartup))]
namespace acaShare.WebAPI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}