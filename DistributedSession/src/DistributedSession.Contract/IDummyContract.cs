using System.Threading.Tasks;

namespace DistributedSession.Contract
{
    public interface IDummyContract
    {
        Task<int> IncrementRequestCount();
    }
}
