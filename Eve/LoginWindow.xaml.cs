using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using Eve.Helpers;
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

namespace Eve
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountService accountService = ServicesFactory.GetInstance().CreateIAccountService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Delegate to other window
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if person has account
            Account account = new Account()
            {
                Username = UsernameBox.Text,
                Password = PasswordBox.Password
            };
            Account dbAccount = await accountService.GetByUniqueIdentifiers(new string[] { "Username" }, account);
            if (dbAccount != null && dbAccount.Password == account.Password)
                WindowHelper.ShowWindow(this, new LoginWindow());
            else
            {
                // Write message and clear fields
                MessageBox.Show("Account is not valid. Fix errors.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PasswordBox.Password = string.Empty;
                UsernameBox.Text = string.Empty;
            }
            // TODO: Delegate to other window
        }
    }
}
