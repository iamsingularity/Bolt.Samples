using System.Threading.Tasks;

namespace Bolt.Console.Sample
{
    public interface IDummyContract
    {
        Task<string> ExecuteAsync(string dummyData);
    }
}