using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;

[assembly: OwinStartup(typeof(ProjectX.WebAPI.Startup1))]
namespace ProjectX.WebAPI
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("ProjectXConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
