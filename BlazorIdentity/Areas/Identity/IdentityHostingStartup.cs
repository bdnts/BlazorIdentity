using System;
using BlazorIdentity.Areas.Identity.Data;
using BlazorIdentity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BlazorIdentity.Areas.Identity.IdentityHostingStartup))]
namespace BlazorIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BlazorIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BlazorIdentityContextConnection")));

                services.AddDefaultIdentity<BlazorIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BlazorIdentityContext>();
            });
        }
    }
}