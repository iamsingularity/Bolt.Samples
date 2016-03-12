using Bolt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoService.Contracts
{
    public interface IMemoService
    {
        [InitSession]
        Task LoginAsync(string userName);

        Task AddMemoAsync(string memo);

        Task RemoveMemoAsync(string memo);

        Task<List<string>> GetAllMemosAsync();

        [DestroySession]
        Task LogoffAsync();
    }
}
