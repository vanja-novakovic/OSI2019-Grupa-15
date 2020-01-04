using Eve.DataGridControls;
using Eve.Helpers;
using Eve.Shared.Config;
using System.Windows;
using System.Windows.Controls;

namespace Eve.IndexControls
{
    /// <summary>
    /// Interaction logic for IndexControl.xaml
    /// </summary>
    /// //odgovara za logiku indeks stranice nekog entiteta: sadrzi tabelu sa podacima i delegira kontrolu ka entiteski specicnom elementu
    public partial class IndexControl : UserControl
    {
        public IndexControlElement IndexControlElement { get; set; }
        public DataGridControl DataGridControl { get; set; }

        private readonly Visibility detailsBtnVisibility;
        private readonly Visibility crudBtnVisibility;
        public IndexControl(IndexControlElement indexControlElement, DataGridControlElement dataGridControlElement, Visibility detailsBtnVisibility = Visibility.Hidden, Visibility crudBtnVisibility = Visibility.Visible)
        {
            InitializeComponent();
            this.IndexControlElement = indexControlElement;
            this.DataGridControl = new DataGridControl(dataGridControlElement);
            DataContext = DataGridControl.DataGrid;
            this.detailsBtnVisibility = detailsBtnVisibility;
            this.crudBtnVisibility = crudBtnVisibility;
            InitializeComponents();

        }

        private void InitializeComponents()
        {
            InitializeDataGrid();
            InitializeButtons();
        }
        private void InitializeDataGrid()
        {

            DataGridControl.DataGrid.IsReadOnly = true;

            DataGridControl.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.Children.Add(DataGridControl);
            Grid.SetRow(DataGridControl, 0);
            Grid.SetColumn(DataGridControl, 1);

            Grid.SetRowSpan(DataGridControl, 2);
            Grid.SetColumnSpan(DataGridControl, 2);
        }
        private void InitializeButtons()
        {
            ButtonContentHelper.SetStackPaneledButton(GoBack.GoBackButton.Content as StackPanel, GoBack.GoBackText, "GoBack");
            SetButtonsVisibiltiy();
        }
        private void SetButtonsVisibiltiy()
        {
            DetailsButton.Visibility = detailsBtnVisibility;
            EditButton.Visibility = CreateButton.Visibility = DeleteButton.Visibility = crudBtnVisibility;

        }
        public void SetBorder(double height, double width)
        {
            Height = height;
            Width = width;
            DataGridControl.Width = Width * (2.0 / 3);
            DataGridControl.Height = Height;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Create(Height, Width);
            DataGridControl.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Delete(DataGridControl.DataGrid.SelectedItem, Height, Width);
            DataGridControl.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Edit(DataGridControl.DataGrid.SelectedItem, Height, Width);
            DataGridControl.Refresh();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Details(DataGridControl.DataGrid.SelectedItem, Height, Width);
            DataGridControl.Refresh();
        }
    }
}
