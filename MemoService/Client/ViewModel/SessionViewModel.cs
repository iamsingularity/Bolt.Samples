using System;
using System.Collections.ObjectModel;
using Bolt.Client;
using Client.Bolt;
using Contract;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Server;

namespace Client.ViewModel
{
    public class SessionViewModel : ViewModelBase
    {
        private readonly ClientConfiguration _configuration;
        private IMemoService _proxy;
        private string _memo;

        public SessionViewModel(string user, ClientConfiguration configuration)
        {
            User = user;
            _configuration = configuration;
            _proxy =
                _configuration.CreateProxy<MemoServiceProxy>(new MemoServiceChannel(user, new Uri(ServerConstants.ServerUrl),
                    configuration));

            Memos = new ObservableCollection<string>();

            LoadCommand = new RelayCommand(() =>
            {
                Memos.Clear();

                foreach (string memo in _proxy.GetAllMemos())
                {
                    Memos.Add(memo);
                }
            });

            AddCommand = new RelayCommand(() =>
            {
                _proxy.AddMemo(Memo);
                Memos.Add(Memo);
            }, () => !string.IsNullOrEmpty(Memo));
        }

        public ObservableCollection<string> Memos { get; set; }

        public string User { get; private set; }

        public string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
                RaisePropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand LoadCommand { get; set; }

        public override void Cleanup()
        {
            ((IDisposable)_proxy).Dispose();
            base.Cleanup();
        }
    }
}