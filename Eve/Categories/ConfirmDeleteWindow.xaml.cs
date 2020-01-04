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

namespace Eve.Categories
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        private readonly CategoryViewModel category;
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();

        public ConfirmDeleteWindow(CategoryViewModel category)
        {
            this.category = category;
            InitializeComponent();
        }

        private async void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Category dbCategory = Mapping.Mapper.Map<Category>(category);
            DbStatus status = await categoryService.Delete(dbCategory);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
