using Bolt.Client;
using Bolt.Helpers;
using Client.ViewModel;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ProtocolBufferSerializer serializer = new ProtocolBufferSerializer();
            DataContext =
                new MainViewModel(new ClientConfiguration(serializer, new JsonExceptionSerializer(serializer),
                    new DefaultWebRequestHandlerEx()));
        }
    }
}
