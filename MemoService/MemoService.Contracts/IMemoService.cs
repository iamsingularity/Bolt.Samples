using Bolt;
using System.Collections.Generic;

namespace MemoService.Contracts
{
    public interface IMemoService
    {
        [InitSession]
        void Login(string userName);

        void AddMemo(string memo);

        void RemoveMemo(string memo);

        List<string> GetAllMemos();

        [DestroySession]
        void Logoff();
    }
}
