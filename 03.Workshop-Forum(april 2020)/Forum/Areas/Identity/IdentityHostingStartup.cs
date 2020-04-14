using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Forum.Areas.Identity.IdentityHostingStartup))]

namespace Forum.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
