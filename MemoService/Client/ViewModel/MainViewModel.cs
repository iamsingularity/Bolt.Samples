using Bolt.Client;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Client.ViewModel
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
                SessionWindow w = new SessionWindow();
                w.Width = 640;
                w.Height = 480;
                w.DataContext = vm;
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