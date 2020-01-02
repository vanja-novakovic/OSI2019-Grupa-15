using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.DataGridControls;
using Eve.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Eve.Accounts
{
    public class DataGridControlElementAccount : DataGridControlElement
    {
        private readonly IAccountService accountService = ServicesFactory.GetInstance().CreateIAccountService();
        public async override Task<int> GetNumberOfItems()
        {
            return await accountService.GetTotalNumberOfItems();
        }

        public DataGridControlElementAccount() : base()
        {
        }
        public DataGridControlElementAccount(DataGrid dataGrid, Label pageInfo, int totalNumberOfItems) : base(dataGrid, pageInfo, totalNumberOfItems)
        {
        }
        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
            {
                if (column.SortMemberPath.StartsWith("Id"))
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }

        protected async override Task<IList> GetData(int index, int number)
        {
            List<Account> list = await accountService.GetRange(index, number) as List<Account>;

            List<AccountViewModel> modelList = list.Select(x => AutoMapper.Mapping.Mapper.Map<AccountViewModel>(x)).ToList();
            return modelList;
        }

        protected override Type GetDataType()
        {
            return typeof(AccountViewModel);
        }
    }
}
