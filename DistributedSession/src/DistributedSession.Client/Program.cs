using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Bolt.Client;
using Bolt.Client.Channels;
using Bolt.Client.Filters;
using Bolt.Client.Proxy;

using DistributedSession.Contract;

namespace DistributedSession.Client
{
    public class Program
    {
        public void Main(string[] args)
        {
            IServerProvider serverProvider = new RandomServerProvider(new Uri("http://localhost:5000"), new Uri("http://localhost:5001"));
            ClientConfiguration configuration = new ClientConfiguration()
                .UseDynamicProxy()
                .AddFilter<LogServerFilter>();

            IDummyContract proxy1 = CreateProxy(configuration, serverProvider);
            TestProxy(proxy1).GetAwaiter().GetResult();

            IDummyContract proxy2 = CreateProxy(configuration, serverProvider);
            TestProxy(proxy2).GetAwaiter().GetResult();

            Console.WriteLine();
        }

        private static IDummyContract CreateProxy(ClientConfiguration configuration, IServerProvider serverProvider)
        {
            IDummyContract proxy = configuration.CreateSessionProxy<IDummyContract>(serverProvider);
            ((IChannel)proxy).Recoverable(10, TimeSpan.FromSeconds(2)).GetSessionChannel().WithDistributedSession();
            return proxy;
        }

        public async Task TestProxy(IDummyContract proxy)
        {
            Console.WriteLine("Sending {0} requests to multiple servers with session", 10);

            int numRequests = 0;
            for (int i = 0; i < 10; i++)
            {
                numRequests = await proxy.IncrementRequestCount();
                Console.WriteLine();

                await Task.Delay(TimeSpan.FromSeconds(0.2));
            }

            if (numRequests != 10)
            {
                throw new InvalidOperationException(
                    $"Distributed session failed. Expected number of requests processed: {10}, Actual: {numRequests}");
            }

            Console.WriteLine("Test finished. Press any key to continue ... ");
        }

        private class RandomServerProvider : IServerProvider
        {
            private readonly Uri[] _servers;

            public RandomServerProvider(params Uri[] servers)
            {
                _servers = servers;
            }

            public ConnectionDescriptor GetServer()
            {
                var server = _servers[new Random().Next(0, _servers.Length)];
                return new ConnectionDescriptor(server);
            }

            public void OnServerUnavailable(Uri server)
            {
            }
        }

        private class LogServerFilter : IClientExecutionFilter
        {
            public async Task ExecuteAsync(ClientActionContext context, Func<ClientActionContext, Task> next)
            {
                Console.WriteLine("Sending request to server: {0}:{1}", context.Request.RequestUri.Host, context.Request.RequestUri.Port);
                try
                {
                    await next(context);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to send request to server: {0}:{1}", context.Request.RequestUri.Host, context.Request.RequestUri.Port);
                    throw;
                }
            }
        }
    }
}
