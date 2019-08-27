using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FilmStore.WEB.Areas.Identity.IdentityHostingStartup))]
namespace FilmStore.WEB.Areas.Identity
{
  public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}