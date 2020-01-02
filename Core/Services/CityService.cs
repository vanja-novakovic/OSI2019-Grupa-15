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
    public class CityService : ICityService
    {
        public async Task<DbStatus> Add(City entity)
        {
            // Unique attributes
            string[] uniqueAttributes = new string[] { "Name" };

            // Check if already exists
            City existingCity = await GetByUniqueIdentifiers(uniqueAttributes, entity);
            if (existingCity != null)
                return DbStatus.EXISTS;

            // Create command for inserting this entity in database
            DbCommand<City> insertCommand = new InsertCommand<City>();
            DbStatus addingStatus = await ServiceHelper<City>.ExecuteCRUDCommand(insertCommand, entity);
            return addingStatus;
        }

        public async Task<DbStatus> Delete(City entity)
        {
            // Check if exists
            City city = await GetByPrimaryKey(entity);
            if (city == null)
                return DbStatus.NOT_FOUND;

            // Delete entity with command
            DbCommand<City> deleteCommand = new CompletelyDeleteCommand<City>();
            DbStatus deleteStatus = await ServiceHelper<City>.ExecuteCRUDCommand(deleteCommand, entity);
            return deleteStatus;
        }

        public async Task<IList<City>> GetAll()
        {
            DbCommand<City> selectAllCommand = new SelectAllCommand<City>();
            return await ServiceHelper<City>.ExecuteSelectCommand(selectAllCommand);
        }

        public async Task<City> GetByPrimaryKey(City entity)
        {
            DbCommand<City> selectWithPrimaryKeyCommand = new SelectWithPrimaryKeyCommand<City>();
            List<City> cities = await ServiceHelper<City>.ExecuteSelectCommand(selectWithPrimaryKeyCommand, entity);
            return cities.Count == 0 ? null : cities[0];
        }

        public async Task<City> GetByUniqueIdentifiers(string[] propertyNames, City entity)
        {
            DbCommand<City> selectWithUniqueAttributes = new SelectWithAttributeValuesCommand<City>(propertyNames);
            List<City> cities = await ServiceHelper<City>.ExecuteSelectCommand(selectWithUniqueAttributes, entity);
            return cities.Count == 0 ? null : cities[0];
        }

        public async Task<IList<City>> GetRange(int begin, int count)
        {
            return await ServiceHelper<City>.ExecuteSelectCommand(new SelectWithRangeCommand<City>(begin, count, "Name"));
        }

        public async Task<int> GetTotalNumberOfItems()
        {
            return Convert.ToInt32(await ServiceHelper<City>.ExecuteScalarCommand(new CountCommand<City>()));
        }

        public async Task<DbStatus> Update(City entity)
        {
            // Check if exists
            City existingCity = await GetByPrimaryKey(entity);
            if (existingCity == null)
                return DbStatus.NOT_FOUND;

            // Check if it violates unique constraint
            City cityWithSameName = await GetByUniqueIdentifiers(new string[] { "Name" }, entity);
            if (cityWithSameName != null && existingCity.Name != entity.Name)
                return DbStatus.EXISTS;

            // Update entity
            DbCommand<City> updateCommand = new UpdateCommand<City>();
            DbStatus updateStatus = await ServiceHelper<City>.ExecuteCRUDCommand(updateCommand, entity);
            return updateStatus;
        }
    }
}
