using System;
using CapgeminiElections.Areas.Identity.Data;
using CapgeminiElections.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CapgeminiElections.Areas.Identity.IdentityHostingStartup))]
namespace CapgeminiElections.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CapgeminiElectionsContext>(options =>
                    options.UseSqlServer("Server=tcp:capgeminielections.database.windows.net,1433;Initial Catalog=CapgeminiElectionUsers;Persist Security Info=False;User ID=ad;Password=Gamecocks2019;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

                services.AddDefaultIdentity<CapgeminiElectionsUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CapgeminiElectionsContext>();
            });
        }
    }
}