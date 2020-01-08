using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using Eve.AutoMapper;
using Eve.ViewModel;
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

namespace Eve.Events
{
    /// <summary>
    /// Interaction logic for EditEvent.xaml
    /// </summary>
    public partial class EditEvent : Window
    {
        private readonly IAddressService addressService = ServicesFactory.GetInstance().CreateIAddressService();
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();
        private readonly ICityService cityService = ServicesFactory.GetInstance().CreateICityService();
        private readonly IEventService eventService = ServicesFactory.GetInstance().CreateIEventService();
        private readonly IAddressCityService addressCityService = ServicesFactory.GetInstance().CreateIAddressCityService();

        private readonly EventViewModel eventModel;

        public EditEvent(EventViewModel eventModel)
        {
            this.eventModel = eventModel;
            InitializeComponent();
            InitializeComponents();
            PopulateForm();
        }

        private void PopulateForm()
        {
            Name.Text = eventModel.Name;
            Description.Text = eventModel.Description;
            Organizers.Text = eventModel.Organizers;
            Duration.Text = eventModel.Duration.ToString();
            foreach (var address in Address.Items)
            {
                if (((AddressViewModel)address).IdAddress == eventModel.IdAddress)
                    Address.SelectedItem = address;
            }
            foreach (var category in Category.Items)
            {
                if (((CategoryViewModel)category).IdCategory == eventModel.IdCategory)
                    Category.SelectedItem = category;
            }
            Time.Text = eventModel.ScheduledOn.TimeOfDay.ToString().Substring(0, 5);
            Date.SelectedDate = eventModel.ScheduledOn.Date;
        }

        private async void InitializeComponents()
        {
            List<Address> addresses = await addressCityService.GetAllInCity(Shared.Config.Properties.Default.City) as List<Address>;
            List<AddressViewModel> addressModels = Mapping.Mapper.Map<List<Address>, List<AddressViewModel>>(addresses);
            foreach (var add in addressModels)
                Address.Items.Add(add);
            List<Category> categories = await categoryService.GetAll() as List<Category>;
            foreach (var cat in categories)
                Category.Items.Add(Mapping.Mapper.Map<CategoryViewModel>(cat));
        }
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime scheduledOn = (DateTime)Date.SelectedDate;
            DateTime time = DateTime.ParseExact(Time.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            scheduledOn = scheduledOn.Add(time.TimeOfDay);

            int idCity = (await cityService.GetByUniqueIdentifiers(new string[] { "Name" }, new City() { Name = Shared.Config.Properties.Default.City })).IdCity;
            Event @event = new Event()
            {
                IdEvent = eventModel.IdEvent,
                IdCategory = ((CategoryViewModel)Category.SelectedItem).IdCategory,
                IdAddress = ((AddressViewModel)Address.SelectedItem).IdAddress,
                Name = Name.Text,
                Description = Description.Text,
                ScheduledOn = scheduledOn,
                Organizers = Organizers.Text,
                IdCity = idCity,
                Duration = int.Parse(Duration.Text)
            };

            DbStatus status = await eventService.Update(@event);
            if (status == DbStatus.NOT_FOUND)
                ShowMessage("Not found!", "Error");
            else
            {
                ShowMessage("Successfully updated!", "Success");
                this.Close();
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
            Name.Name = string.Empty;
            Address.SelectedItem = null;
            Time.Text = string.Empty;
        }
    }
}
