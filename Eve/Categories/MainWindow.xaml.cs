using Eve.Helpers;
using Eve.IndexControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eve.Categories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRefreshable, IWindowReturnable
    {
        public Window PreviousWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WindowHelper.SetBorder(this, this.Grid);
            IndexControl control = new IndexControl(new IndexControlElementCategory(), new DataGridControlElementCategory());
            control.SetBorder(Height, Width);
            Grid.Children.Add(control);
        }

        public void Refresh()
        {
            WindowHelper.ShowWindow(this, new MainWindow());
        }

        public void ReturnToPreviousWindow()
        {
            WindowHelper.ShowWindow(this, PreviousWindow);
        }

        public void SetReturningWindow(Window window)
        {
            PreviousWindow = window;
        }
    }
}
