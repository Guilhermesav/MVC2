using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC.Apresentação.Data;

[assembly: HostingStartup(typeof(MVC.Apresentação.Areas.Identity.IdentityHostingStartup))]
namespace MVC.Apresentação.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MVCApresentaçãoContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MVCApresentaçãoContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MVCApresentaçãoContext>();
            });
        }
    }
}