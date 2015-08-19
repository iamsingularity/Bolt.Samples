using Bolt.Client;

using Client;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MemoService.Client
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ClientConfiguration _configuration;
        private string _user;

        public MainViewModel(ClientConfiguration configuration)
        {
            _configuration = configuration;
            LoginCommand = new RelayCommand(() =>
            {
                SessionViewModel vm = new SessionViewModel(User, _configuration);
                SessionWindow w = new SessionWindow { Width = 640, Height = 480, DataContext = vm };
                w.Show();

            }, () => !string.IsNullOrEmpty(User));
        }

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

        public RelayCommand LoginCommand { get; set; }
    }
}