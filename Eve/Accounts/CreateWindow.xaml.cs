using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using Eve.Helpers;
using Eve.Shared.Config;
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

namespace Eve.Accounts
{
    /// <summary>
    /// Interaction logic for InsertWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        private readonly IAccountService accountService = ServicesFactory.GetInstance().CreateIAccountService();
        public CreateWindow()
        {
            InitializeComponent();
            InitializeFields();
        }

        public void InitializeFields()
        {
            ButtonContentHelper.SetContent(SaveButton, language.Save);
            ButtonContentHelper.SetContent(CancelButton, language.Cancel);
            LabelHelper.SetLabelContent(UsernameLabel, language.Username);
            LabelHelper.SetLabelContent(PasswordLabel, language.Password);
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account()
            {
                Username = UsernameTextBox.Text,
                Password = PasswordPasswordBox.Password
            };
            DbStatus status = await accountService.Add(account);
            MessageLabel.Content = status.ToString();
        }
    }
}
