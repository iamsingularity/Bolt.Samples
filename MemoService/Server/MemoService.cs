using Contract;
using System.Collections.Generic;

namespace Server
{
    public class MemoService : IMemoService
    {
        private readonly List<string> _memos = new List<string>();

        private string _user;

        public void Login(string userName)
        {
            _user = userName;
        }

        public void AddMemo(string memo)
        {
            _memos.Add(memo);
        }

        public void RemoveMemo(string memo)
        {
            _memos.Remove(memo);
        }

        public List<string> GetAllMemos()
        {
            return _memos;
        }

        public void Logoff()
        {
            _user = null;
        }
    }
}