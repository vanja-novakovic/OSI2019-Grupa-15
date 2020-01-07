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
    /// Interaction logic for ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        private readonly IEventService eventService = ServicesFactory.GetInstance().CreateIEventService();
        private readonly EventViewModel eventModel;
        public ConfirmDeleteWindow(EventViewModel eventModel)
        {
            this.eventModel = eventModel;
            InitializeComponent();
        }

        private void ShowMessage(string message, string caption)
        {
            var res = MessageBox.Show(message, caption, MessageBoxButton.OK);
            if (res == MessageBoxResult.OK)
                this.Close();
        }

        private async void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DbStatus status = await eventService.Delete(Mapping.Mapper.Map<Event>(eventModel));
            if (status == DbStatus.NOT_FOUND)
                ShowMessage("Not found! ", "Error");
            else if (status == DbStatus.DATABASE_ERROR)
                ShowMessage("Database error! ", "Error");
            else
                ShowMessage("Successfully deleted! ", "Success");
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
