using System;
using Bolt.Server;
using ContactList.Contracts;
using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ContactList.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddBolt();
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<ContactsDbContext>(c => c.UseInMemoryDatabase());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            app.UseBolt(
                b =>
                {
                    b.Use<IContactListProvider, ContactListProvider>();
                });
        }
    }
}