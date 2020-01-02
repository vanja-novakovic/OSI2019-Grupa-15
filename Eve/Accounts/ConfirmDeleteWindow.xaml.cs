using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using Eve.AutoMapper;
using Eve.Helpers;
using Eve.Shared.Config;
using Eve.ViewModel;
using System.Windows;

namespace Eve.Accounts
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        private readonly IAccountService accountService = ServicesFactory.GetInstance().CreateIAccountService();
        private readonly AccountViewModel accountViewModel;

        public ConfirmDeleteWindow(AccountViewModel accountViewModel)
        {
            this.accountViewModel = accountViewModel;
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ButtonContentHelper.SetContent(YesButton, language.Yes);
            ButtonContentHelper.SetContent(NoButton, language.No);
            LabelHelper.SetLabelContent(QuestionLabel, language.ConfirmDelete);
        }

        /// <summary>
        /// If yes button is clicked, then remove record from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = Mapping.Mapper.Map<Account>(accountViewModel);
            DbStatus status = await accountService.Delete(account);
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
