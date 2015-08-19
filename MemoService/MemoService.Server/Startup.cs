using Bolt.Server;

using MemoService.Contracts;

using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace MemoService.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddBolt();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            app.UseBolt(
                b =>
                    {
                        b.UseMemorySession<IMemoService, MemoService>();
                    });
        }
    }
}