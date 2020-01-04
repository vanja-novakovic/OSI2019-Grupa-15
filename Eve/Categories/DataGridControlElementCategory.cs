using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.AutoMapper;
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

namespace Eve.Categories
{
    public class DataGridControlElementCategory : DataGridControlElement
    {
        private readonly ICategoryService categoryService = ServicesFactory.GetInstance().CreateICategoryService();

        public override async Task<int> GetNumberOfItems()
        {
            return await categoryService.GetTotalNumberOfItems();
        }

        public DataGridControlElementCategory() : base()
        {
        }
        public DataGridControlElementCategory(DataGrid dataGrid, Label pageInfo, int totalNumberOfItems) : base(dataGrid, pageInfo, totalNumberOfItems)
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
            List<Category> categories = await categoryService.GetRange(index, number) as List<Category>;
            List<CategoryViewModel> models = Mapping.Mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return models;
        }

        protected override Type GetDataType()
        {
            return typeof(CategoryViewModel);
        }
    }
}
