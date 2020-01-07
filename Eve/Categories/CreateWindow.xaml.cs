using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
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
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();

        public CreateWindow()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                ShowMessage("Name is required!", "Error");
            }
            else
            {
                Category category = new Category()
                {
                    Name = NameBox.Text
                };
                DbStatus status = await categoryService.Add(category);
                if (status == DbStatus.EXISTS)
                    ShowMessage("Already exists!", "Error");
                else
                {
                    ShowMessage("Successfully added!", "Success");
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
