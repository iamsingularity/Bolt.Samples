using System.Collections.Generic;
using System.Threading.Tasks;
using Bolt;
using Bolt.Server;
using Bolt.Server.Session;
using MemoService.Contracts;
using Microsoft.Extensions.Logging;

namespace MemoService.Server
{
    public class MemoService : IMemoService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly ILogger<MemoService> _logger;
        private readonly List<string> _memos = new List<string>();

        private string _user;

        public MemoService(ISessionProvider sessionProvider, ILogger<MemoService> logger)
        {
            _sessionProvider = sessionProvider;
            _logger = logger;
        }

        public Task LoginAsync(string userName)
        {
            _logger.LogInformation("Login: {0}, Session: {1}", userName, _sessionProvider.SessionId);
            _user = userName;
            return Task.FromResult(true);
        }

        public Task AddMemoAsync(string memo)
        {
            _memos.Add(memo);
            return Task.FromResult(true);
        }

        public Task RemoveMemoAsync(string memo)
        {
            _memos.Remove(memo);
            return Task.FromResult(true);
        }

        public Task<List<string>> GetAllMemosAsync()
        {
            return Task.FromResult(_memos);
        }

        public Task LogoffAsync()
        {
            _logger.LogInformation("Logout: {0}, Session: {1}", _user, _sessionProvider.SessionId);
            _user = null;
            return Task.FromResult(true);
        }
    }
}