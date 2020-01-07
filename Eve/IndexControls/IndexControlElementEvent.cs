using Eve.AutoMapper;
using Eve.Events;
using Eve.Helpers;
using Eve.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eve.IndexControls
{
    public class IndexControlElementEvent
    {
        public void Create(double height = 0.0, double width = 0.0)
        {
            Window createWindow = new CreateEvent();
            WindowHelper.SetModal(createWindow, height, width);
            createWindow.ShowDialog();
        }
        public void Delete(object selectedItem, double height = 0.0, double width = 0.0)
        {
            EventViewModel @event = Mapping.Mapper.Map<EventViewModel>(selectedItem as DataRowView);
            Window confirmDeleteWindow = new ConfirmDeleteWindow(@event);
            WindowHelper.CenterWindow(confirmDeleteWindow, height, width);
            confirmDeleteWindow.ShowDialog();

        }
        public void Edit(object selectedItem, double height = 0.0, double width = 0.0)
        {
            EventViewModel @event = Mapping.Mapper.Map<EventViewModel>(selectedItem as DataRowView);
            Window editWindow = new EditEvent(@event);
            WindowHelper.SetModal(editWindow, height, width);
            editWindow.ShowDialog();
        }
        public void Details(object selectedItem, double height = 0.0, double width = 0.0)
        {
            EventViewModel @event = Mapping.Mapper.Map<EventViewModel>(selectedItem as DataRowView);
            Window win = new DetailsWindow(@event);
            WindowHelper.SetModal(win, height, width);
            win.ShowDialog();

        }
    }
}
