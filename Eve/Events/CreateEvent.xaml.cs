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
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Window
    {
        private readonly IAddressService addressService = ServicesFactory.GetInstance().CreateIAddressService();
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();
        private readonly ICityService cityService = ServicesFactory.GetInstance().CreateICityService();
        private readonly IEventService eventService = ServicesFactory.GetInstance().CreateIEventService();
        private readonly IAddressCityService addressCityService = ServicesFactory.GetInstance().CreateIAddressCityService();


        public CreateEvent()
        {
            InitializeComponent();
            InitializeComponents();
        }

        public async void InitializeComponents()
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
                IdCategory = ((CategoryViewModel)Category.SelectedItem).IdCategory,
                IdAddress = ((AddressViewModel)Address.SelectedItem).IdAddress,
                Name = Name.Text,
                Description = Description.Text,
                ScheduledOn = scheduledOn,
                Organizers = Organizers.Text,
                IdCity = idCity,
                Duration = 60
            };

            DbStatus status = await eventService.Add(@event);
            if (status == DbStatus.EXISTS)
                ShowMessage("Already exists!", "Error");
            else
            {
                ShowMessage("Successfully added!", "Success");
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
