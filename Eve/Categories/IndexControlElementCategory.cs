using Eve.AutoMapper;
using Eve.Helpers;
using Eve.IndexControls;
using Eve.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eve.Categories
{
    public class IndexControlElementCategory : IndexControlElement
    {
        public override void Create(double height = 0, double width = 0)
        {
            Window createWindow = new CreateWindow();
            WindowHelper.SetModal(createWindow, height, width);
            createWindow.ShowDialog();
        }

        public override void Edit(object selectedItem, double height = 0, double width = 0)
        {
            CategoryViewModel category = Mapping.Mapper.Map<CategoryViewModel>(selectedItem as DataRowView);
            Window editWindow = new EditWindow(category);
            WindowHelper.SetModal(editWindow, height, width);
            editWindow.ShowDialog();
        }

        public override void Details(object selectedItem, double height = 0, double width = 0)
        {
            throw new NotImplementedException();
        }

        public override void Delete(object selectedItem, double height = 0, double width = 0)
        {
            CategoryViewModel category = Mapping.Mapper.Map<CategoryViewModel>(selectedItem as DataRowView);
            Window confirmDeleteWindow = new ConfirmDeleteWindow(category);
            WindowHelper.CenterWindow(confirmDeleteWindow, height, width);
            confirmDeleteWindow.ShowDialog();
        }
    }
}
