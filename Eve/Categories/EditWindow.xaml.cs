using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly CategoryViewModel category;
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();

        public EditWindow(CategoryViewModel category)
        {
            this.category = category;
            InitializeComponent();
            FillFields();
        }

        private void FillFields()
        {
            NameBox.Text = category.Name;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                ShowMessage("Name is required!", "Error");
            }
            else
            {
                Category updatedCategory = new Category()
                {
                    Name = NameBox.Text,
                    IdCategory = category.IdCategory
                };
                DbStatus status = await categoryService.Update(updatedCategory);
                if (status == DbStatus.NOT_FOUND)
                    ShowMessage("Not found!", "Error");
                else if (status == DbStatus.EXISTS)
                    ShowMessage("Already exists!", "Error");
                else
                {
                    ShowMessage("Successfully updated!", "Success");
                    this.Close();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
            NameBox.Name = string.Empty;
        }
    }
}
