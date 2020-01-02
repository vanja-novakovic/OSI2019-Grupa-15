using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.AutoMapper;
using Eve.Helpers;
using Eve.IndexControls;
using Eve.ViewModel;
using System.Data;
using System.Windows;

namespace Eve.Accounts
{
    public class IndexControlElementAccount : IndexControlElement
    {

        private readonly IAccountService accountService = ServicesFactory.GetInstance().CreateIAccountService();

        /// <summary>
        /// Opens window for creating new account
        /// </summary>
        /// <param name="height">Current height of parent window</param>
        /// <param name="width">Current width of parent window</param>
        public override void Create(double height = 0, double width = 0)
        {
            Window modalCreate = new CreateWindow();

            // Sets window in center with certain width and height
            WindowHelper.SetModal(modalCreate, height, width);

            // Shows window as dialog, so current window is not closed
            modalCreate.ShowDialog();
        }

        /// <summary>
        /// Creates window for confirmin delete and sends object to its constructor
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public async override void Delete(object selectedItem, double height = 0, double width = 0)
        {
            // Gets selected account from table and converts it into Account object
            DataRowView item = (DataRowView)selectedItem;
            Account account = Mapping.Mapper.Map<Account>(item);

            // Gets account with same properties from database, precisely with same primary key
            account = await accountService.GetByPrimaryKey(account);

            // Creates window for confirming delete and sends object to its constructor, 
            // because it needs to be deleted if user confirms
            Window deleteModal = new ConfirmDeleteWindow(Mapping.Mapper.Map<AccountViewModel>(account));

            // It is a smaller window that window for creating and editing, so it is centered
            WindowHelper.CenterWindow(deleteModal, height, width);
            deleteModal.ShowDialog();
        }

        /// <summary>
        ///  Method for creating window for editing account
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public async override void Edit(object selectedItem, double height = 0, double width = 0)
        {
            // Gets selected account from table and converts it into Account object
            DataRowView item = (DataRowView)selectedItem;
            Account account = Mapping.Mapper.Map<Account>(item);

            // Gets account with same properties from database, precisely with same primary key
            account = await accountService.GetByPrimaryKey(account);

            // Create window for editing selected account. Send that account to its constructor, so it can
            // show its current values of properties and save changes
            Window editModal = new UpdateWindow(Mapping.Mapper.Map<AccountViewModel>(account));
            WindowHelper.SetModal(editModal, height, width);
            editModal.ShowDialog();
        }

        /// <summary>
        /// Opens window for showing details of selected object
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public async override void Details(object selectedItem, double height = 0, double width = 0)
        {
            // Gets selected account from table and converts it into Account object
            DataRowView item = (DataRowView)selectedItem;
            Account account = Mapping.Mapper.Map<Account>(item);

            // Gets account with same properties from database, precisely with same primary key
            account = await accountService.GetByPrimaryKey(account);

            Window detailsModall = new DetailsWindow(Mapping.Mapper.Map<AccountViewModel>(account));
            WindowHelper.SetModal(detailsModall, height, width);
            detailsModall.ShowDialog();
        }
    }
}
