using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.Helpers;
using System.Windows;
using System.Windows.Controls;

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
            WindowHelper.ShowWindow(this, new GuestMode());
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if person has account
            if (PasswordBox.Password == string.Empty || PasswordBox.Password is null
                || UsernameBox.Text == string.Empty || UsernameBox.Text is null)
                WriteMessageAndClearFields("Password and Username are required.", password: PasswordBox.Password, username: UsernameBox.Text);
            else
            {
                Account account = new Account()
                {
                    Username = UsernameBox.Text,
                    Password = PasswordBox.Password
                };
                Account dbAccount = await accountService.GetByUniqueIdentifiers(new string[] { "Username" }, account);
                if (dbAccount != null && dbAccount.Password == account.Password)
                    WindowHelper.ShowWindow(this, new WithAccount());
                else
                    WriteMessageAndClearFields("Account is not valid. Fix errors.");
            }
        }
        private void WriteMessageAndClearFields(string message, string password = "", string username = "")
        {
            // Write message and clear fields
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            PasswordBox.Password = password;
            UsernameBox.Text = username;
        }
    }
}
