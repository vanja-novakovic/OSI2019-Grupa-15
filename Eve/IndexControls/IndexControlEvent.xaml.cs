using Core.Common;
using Eve.DataGridControls;
using Eve.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Eve.IndexControls
{
    /// <summary>
    /// Interaction logic for IndexControlEvent.xaml
    /// </summary>
    public partial class IndexControlEvent : UserControl
    {
        public IndexControlElementEvent IndexControlElementEvent { get; set; }
        public DataGridControlEvent DataGridControlEvent { get; set; }
        public DataGridControlElementEvent DataGridControlElementEvent { get; set; }

        private readonly Visibility detailsBtnVisibility;
        private readonly Visibility crudBtnVisibility;
        public IndexControlEvent(IndexControlElementEvent indexControlElementEvent, DataGridControlElementEvent dataGridControlElementEvent,
            Visibility detailsBtnVisibility = Visibility.Hidden, Visibility crudBtnVisibility = Visibility.Visible)
        {
            InitializeComponent();
            this.IndexControlElementEvent = indexControlElementEvent;
            this.DataGridControlElementEvent = dataGridControlElementEvent;
            this.DataGridControlEvent = new DataGridControlEvent(dataGridControlElementEvent);
            DataContext = DataGridControlEvent.DataGrid;
            this.detailsBtnVisibility = detailsBtnVisibility;
            this.crudBtnVisibility = crudBtnVisibility;
            InitializeComponents();

        }

        private void InitializeComponents()
        {
            InitializeDataGrid();
            InitializeButtons();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            FilterComboBox.Items.Add(EventFilter.NONE);
            FilterComboBox.Items.Add(EventFilter.FUTURE);
            FilterComboBox.Items.Add(EventFilter.PRESENT);
            FilterComboBox.Items.Add(EventFilter.CATEGORY);

            SortComboBox.Items.Add("ScheduledOn");
            SortComboBox.Items.Add("Name");
        }

        private void InitializeDataGrid()
        {

            DataGridControlEvent.DataGrid.IsReadOnly = true;

            DataGridControlEvent.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.Children.Add(DataGridControlEvent);
            Grid.SetRow(DataGridControlEvent, 0);
            Grid.SetColumn(DataGridControlEvent, 1);

            Grid.SetRowSpan(DataGridControlEvent, 2);
            Grid.SetColumnSpan(DataGridControlEvent, 2);
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
            DataGridControlEvent.Width = Width * (2.0 / 3);
            DataGridControlEvent.Height = Height;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElementEvent.Create(Height, Width);
            DataGridControlEvent.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElementEvent.Delete(DataGridControlEvent.DataGrid.SelectedItem, Height, Width);
            DataGridControlEvent.Refresh();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElementEvent.Edit(DataGridControlEvent.DataGrid.SelectedItem, Height, Width);
            DataGridControlEvent.Refresh();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElementEvent.Details(DataGridControlEvent.DataGrid.SelectedItem, Height, Width);
            DataGridControlEvent.Refresh();
        }

        private async void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventFilter eventFilter = (EventFilter)FilterComboBox.SelectedItem;
            string orderBy = SortComboBox.SelectedItem == null ? null : (string)(SortComboBox.SelectedItem);
            await DataGridControlElementEvent.Refresh(eventFilter, null, orderBy);
        }

        private async void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventFilter eventFilter = FilterComboBox.SelectedItem == null ? EventFilter.NONE : (EventFilter)(FilterComboBox.SelectedItem);
            string orderBy = (string)(SortComboBox.SelectedItem);
            await DataGridControlElementEvent.Refresh(eventFilter, null, orderBy);
        }
    }
}
