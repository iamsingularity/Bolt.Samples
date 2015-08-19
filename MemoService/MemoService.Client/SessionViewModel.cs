using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

using Bolt.Client;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MemoService.Contracts;

namespace MemoService.Client
{
    public class SessionViewModel : ViewModelBase
    {
        private readonly IMemoService _proxy;
        private string _memo;

        public SessionViewModel(string user, ClientConfiguration configuration)
        {
            User = user;
            _proxy =
                configuration.ProxyBuilder()
                    .Url("http://localhost:5000")
                    .UseSession()
                    .Recoverable(10, TimeSpan.FromSeconds(1))
                    .Build<IMemoService>();
            _proxy.Login(user);

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

            TestCommand = new RelayCommand(async () =>
            {
                int repeats = Repeats;

                long elapsed = await Task.Run(() =>
                {
                    Stopwatch watch = Stopwatch.StartNew();

                    for (int i = 0; i < repeats; i++)
                    {
                        _proxy.GetAllMemos();
                    }

                    return watch.ElapsedMilliseconds;
                });

                MessageBox.Show(string.Format("Requesting all memos {0} times has taken {1}ms.", repeats, elapsed), "Performance Result", MessageBoxButton.OK);
            });
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

        public RelayCommand TestCommand { get; set; }

        public int Repeats { get; set; }

        public override void Cleanup()
        {
            (_proxy as IDisposable)?.Dispose();
            base.Cleanup();
        }
    }
}