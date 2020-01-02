using Eve.Helpers;
using Eve.IndexControls;
using System.Windows;
using System.Windows.Controls;
namespace Eve.Accounts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRefreshable
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowHelper.SetBorder(this, this.Grid);
            IndexControl control = new IndexControl(new IndexControlElementAccount(), new DataGridControlElementAccount(), detailsBtnVisibility: Visibility.Visible, crudBtnVisibility: Visibility.Visible);
            control.SetBorder(Height, Width);
            Grid.Children.Add(control);
        }

        public void Refresh()
        {
            WindowHelper.ShowWindow(this, new MainWindow());
        }
    }
}
