using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.AutoMapper;
using Eve.DataGridControls;
using Eve.Helpers;
using Eve.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Eve.IndexControls
{
    /// <summary>
    /// Interaction logic for IndexControlEvent.xaml
    /// </summary>
    public partial class IndexControlEvent : UserControl
    {
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();

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

        private async void InitializeComboBoxes()
        {
            FilterComboBox.Items.Add(EventFilter.NONE);
            FilterComboBox.Items.Add(EventFilter.FUTURE);
            FilterComboBox.Items.Add(EventFilter.PRESENT);
            FilterComboBox.Items.Add(EventFilter.CATEGORY);

            SortComboBox.Items.Add("ScheduledOn [Earliest first]");
            SortComboBox.Items.Add("ScheduledOn [Latest first]");
            SortComboBox.Items.Add("Name [A-Z]");
            SortComboBox.Items.Add("Name [Z-A]");

            List<Category> categories = await categoryService.GetAll() as List<Category>;
            foreach (var cat in categories)
                CategoryComboBox.Items.Add(Mapping.Mapper.Map<CategoryViewModel>(cat));
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

            (string orderBy, string order) = GetOrder();

            if (eventFilter == EventFilter.CATEGORY)
            {
                CategoryLabel.Visibility = Visibility.Visible;
                CategoryComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                CategoryLabel.Visibility = Visibility.Hidden;
                CategoryComboBox.Visibility = Visibility.Hidden;
                await DataGridControlElementEvent.Refresh(eventFilter, null, orderBy, order);
            }
        }

        private async void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventFilter eventFilter = FilterComboBox.SelectedItem == null ? EventFilter.NONE : (EventFilter)(FilterComboBox.SelectedItem);
            (string orderBy, string order) = GetOrder();

            await DataGridControlElementEvent.Refresh(eventFilter, null, orderBy, order);

        }

        private async void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            (string orderBy, string order) = GetOrder();

            EventFilter eventFilter = EventFilter.CATEGORY;
            CategoryViewModel category = (CategoryViewModel)CategoryComboBox.SelectedItem;
            await DataGridControlElementEvent.Refresh(eventFilter, category.IdCategory, orderBy, order);
        }

        private (string orderBy, string order) GetOrder()
        {
            string orderBy = SortComboBox.SelectedItem == null ? "Name [A-Z]" : (string)(SortComboBox.SelectedItem);
            string order = "asc";
            switch (orderBy)
            {
                case "ScheduledOn [Earliest first]": order = "asc"; orderBy = "ScheduledOn"; break;
                case "ScheduledOn [Latest first]": order = "desc"; orderBy = "ScheduledOn"; break;
                case "Name [A-Z]": order = "asc"; orderBy = "Name"; break;
                default: order = "desc"; orderBy = "Name"; break;
            }
            return (orderBy, order);
        }
    }
}
