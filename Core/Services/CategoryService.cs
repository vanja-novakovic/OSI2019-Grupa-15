using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task<DbStatus> Add(Category entity)
        {
            // Category with same name must not exist in database
            string[] uniqueAttributes = new string[] { "Name" };
            Category existingCategory = await GetByUniqueIdentifiers(uniqueAttributes, entity);
            if (existingCategory != null)
                return DbStatus.EXISTS;
            // Execute insert command in table Category
            DbCommand<Category> insertCommand = new InsertCommand<Category>();
            DbStatus statusOfExecution = await ServiceHelper<Category>.ExecuteCRUDCommand(insertCommand, entity);
            return statusOfExecution;
        }

        public async Task<DbStatus> Delete(Category entity)
        {
            // Category with specified id doesn't exists
            Category existingCategory = await GetByPrimaryKey(entity);
            if (existingCategory == null)
                return DbStatus.NOT_FOUND;

            DbCommand<Category> deleteCommand = new CompletelyDeleteCommand<Category>();
            DbStatus statusOfExecution = await ServiceHelper<Category>.ExecuteCRUDCommand(deleteCommand, entity);
            return statusOfExecution;
        }

        public async Task<IList<Category>> GetAll()
        {
            return await ServiceHelper<Category>.ExecuteSelectCommand(new SelectAllCommand<Category>());
        }

        public async Task<Category> GetByPrimaryKey(Category entity)
        {
            var list = await ServiceHelper<Category>.ExecuteSelectCommand(new SelectWithPrimaryKeyCommand<Category>(), entity);
            // If no object is found, return null
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<Category> GetByUniqueIdentifiers(string[] propertyNames, Category entity)
        {
            var list = await ServiceHelper<Category>.ExecuteSelectCommand(new SelectWithAttributeValuesCommand<Category>(propertyNames), entity);
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<IList<Category>> GetRange(int begin, int count)
        {
            return await ServiceHelper<Category>.ExecuteSelectCommand(new SelectWithRangeCommand<Category>(begin, count, "Name"));
        }

        public async Task<int> GetTotalNumberOfItems()
        {
            return Convert.ToInt32(await ServiceHelper<Category>.ExecuteScalarCommand(new CountCommand<Category>()));
        }

        public async Task<DbStatus> Update(Category entity)
        {
            // Category with specified id doesn't exists
            Category existingCategory = await GetByPrimaryKey(entity);
            if (existingCategory == null)
                return DbStatus.NOT_FOUND;

            Category categoryWithSameName = await GetByUniqueIdentifiers(new string[] { "Name" }, entity);
            if (categoryWithSameName != null && existingCategory.Name != entity.Name)
                return DbStatus.EXISTS;

            DbCommand<Category> updateCommand = new UpdateCommand<Category>();
            DbStatus status = await ServiceHelper<Category>.ExecuteCRUDCommand(updateCommand, entity);
            return status;
        }
    }
}
