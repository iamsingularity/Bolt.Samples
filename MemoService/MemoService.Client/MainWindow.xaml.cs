using System.Windows;

using Bolt.Client;

namespace MemoService.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new ClientConfiguration());
        }
    }
}
