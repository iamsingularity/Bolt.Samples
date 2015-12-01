using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace Bolt.Console.Sample
{
    public class DummyContract : IDummyContract
    {
        private readonly ILogger<DummyContract> _logger;

        public DummyContract(ILogger<DummyContract> logger)
        {
            _logger = logger;
        }

        public Task<string> ExecuteAsync(string dummyData)
        {
            _logger.LogDebug("Executing Bolt contract ... ");

            return Task.FromResult(dummyData);
        }
    }
}