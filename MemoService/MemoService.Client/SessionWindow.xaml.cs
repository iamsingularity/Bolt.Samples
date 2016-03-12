using System.ComponentModel;

namespace MemoService.Client
{
    /// <summary>
    /// Interaction logic for SessionWindow.xaml
    /// </summary>
    public partial class SessionWindow
    {
        public SessionWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SessionViewModel vm = DataContext as SessionViewModel;
            vm?.Cleanup();

            base.OnClosing(e);
        }
    }
}
