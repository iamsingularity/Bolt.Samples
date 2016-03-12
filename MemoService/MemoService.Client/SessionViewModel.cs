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
                    .Recoverable(5, TimeSpan.FromSeconds(1))
                    .Build<IMemoService>();

            Memos = new ObservableCollection<string>();

            LoadCommand = new RelayCommand(async () =>
            {
                Memos.Clear();

                foreach (string memo in await _proxy.GetAllMemosAsync())
                {
                    Memos.Add(memo);
                }
            });

            AddCommand = new RelayCommand(async () =>
            {
                await _proxy.AddMemoAsync(Memo);
                Memos.Add(Memo);
            }, () => !string.IsNullOrEmpty(Memo));

            TestCommand = new RelayCommand(async () =>
            {
                int repeats = Repeats;

                long elapsed = await Task.Run(async () =>
                {
                    Stopwatch watch = Stopwatch.StartNew();

                    for (int i = 0; i < repeats; i++)
                    {
                        await _proxy.GetAllMemosAsync();
                    }

                    return watch.ElapsedMilliseconds;
                });

                MessageBox.Show($"Requesting all memos {repeats} times has taken {elapsed}ms.", "Performance Result", MessageBoxButton.OK);
            });
        }

        public Task LoginAsync()
        {
            return _proxy.LoginAsync(User);
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