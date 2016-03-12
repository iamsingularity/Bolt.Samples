using Bolt.Client;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MemoService.Client
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ClientConfiguration _configuration;
        private string _user;
        private bool _isExecuting;

        public MainViewModel(ClientConfiguration configuration)
        {
            _configuration = configuration;
            LoginCommand = new RelayCommand(async () =>
            {
                try
                {
                    IsExecuting = true;
                    LoginCommand.RaiseCanExecuteChanged();
                    SessionViewModel vm = new SessionViewModel(User, _configuration);
                    await vm.LoginAsync();
                    SessionWindow w = new SessionWindow {Width = 640, Height = 480, DataContext = vm};
                    w.Show();
                    User = null;
                }
                finally
                {
                    IsExecuting = false;
                }

            }, () => !string.IsNullOrEmpty(User) && !IsExecuting);
        }

        public string LoginCaption => IsExecuting ? "Logging ... " : "Login";

        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                LoginCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        private bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                LoginCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LoginCaption));
            }
        }

        public RelayCommand LoginCommand { get; }
    }
}