using Bolt;
using System.Collections.Generic;

namespace Contract
{
    public interface IMemoService
    {
        [InitSession]
        void Login(string userName);

        void AddMemo(string memo);

        void RemoveMemo(string memo);

        List<string> GetAllMemos();

        [CloseSession]
        void Logoff();
    }
}
