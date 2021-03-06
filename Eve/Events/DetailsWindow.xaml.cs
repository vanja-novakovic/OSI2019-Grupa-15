﻿using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Eve.Events
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();
        private readonly IAddressCityService addressCityService = ServicesFactory.GetInstance().CreateIAddressCityService();

        private readonly EventViewModel eventModel;

        public DetailsWindow(EventViewModel eventModel)
        {
            this.eventModel = eventModel;
            InitializeComponent();
            PopulateForm();
        }

        private async void PopulateForm()
        {
            List<Address> addresses = await addressCityService.GetAllInCity(Shared.Config.Properties.Default.City) as List<Address>;
            Address.Text = addresses.Where(x => x.IdAddress == eventModel.IdAddress).First().Name;
            List<Category> categories = await categoryService.GetAll() as List<Category>;
            Category.Text = categories.Where(x => x.IdCategory == eventModel.IdCategory).First().Name;
            Address.IsEnabled = false;
            Category.IsEnabled = false;
            Description.Text = eventModel.Description;
            Organizers.Text = eventModel.Organizers;
            Name.Text = eventModel.Name;
            Name.IsEnabled = false;
            Time.Text = eventModel.ScheduledOn.TimeOfDay.ToString().Substring(0, 5);
            Time.IsEnabled = false;
            Date.SelectedDate = eventModel.ScheduledOn.Date;
            Date.IsEnabled = false;
            Organizers.IsEnabled = false;
            Description.IsEnabled = false;
            Duration.Text = eventModel.Duration.ToString();
            Duration.IsEnabled = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
