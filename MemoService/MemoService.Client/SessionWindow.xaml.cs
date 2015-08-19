using System.ComponentModel;
using System.Windows;

using MemoService.Client;

namespace Client
{
    /// <summary>
    /// Interaction logic for SessionWindow.xaml
    /// </summary>
    public partial class SessionWindow : Window
    {
        public SessionWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SessionViewModel vm = DataContext as SessionViewModel;
            if (vm != null)
            {
                vm.Cleanup();
            }

            base.OnClosing(e);
        }
    }
}
