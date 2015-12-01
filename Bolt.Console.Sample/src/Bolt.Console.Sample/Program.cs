using System;
using System.Threading.Tasks;

using Bolt.Client;
using Bolt.Client.Proxy;
using Bolt.Server;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bolt.Console.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddBolt();
                serviceCollection.AddOptions();
                serviceCollection.AddLogging();
            }

            public void Configure(IApplicationBuilder builder)
            {
                ILoggerFactory factory = builder.ApplicationServices.GetRequiredService<ILoggerFactory>();
                factory.MinimumLevel = LogLevel.Debug;
                factory.AddConsole();

                // we will add IDummyContract endpoint to Bolt
                builder.UseBolt(r => r.Use<IDummyContract, DummyContract>());
                TestBolt(builder.ApplicationServices.GetRequiredService<ILogger<Program>>());
            }
        }


        private static async void TestBolt(ILogger<Program> logger)
        {
            await Task.Delay(2500);

            // create Bolt proxy
            ClientConfiguration configuration = new ClientConfiguration().UseDynamicProxy();
            IDummyContract proxy = configuration.CreateProxy<IDummyContract>("http://localhost:5000");

            logger.LogInformation("Testing Bolt proxy ... ");

            for (int i = 0; i < 10; i++)
            {
                logger.LogInformation("Sending #{0} request from Bolt.", i);
                // we can add timeout and CancellationToken to each Bolt call
                using (new RequestScope(TimeSpan.FromSeconds(5)))
                {

                    try
                    {
                        await proxy.ExecuteAsync(i.ToString());
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Error: {0}", e);
                    }
                }

                logger.LogInformation("#{0} request finished.", i);
                await Task.Delay(1000);
            }

            logger.LogInformation("Testing Bolt proxy finished");
        }
    }
}
