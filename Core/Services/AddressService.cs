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
    public class AddressService : IAddressService
    {
        public async Task<DbStatus> Add(Address entity)
        {
            
            // Execute insert command in table Address
            DbCommand<Address> insertCommand = new InsertCommand<Address>();
            DbStatus statusOfExecution = await ServiceHelper<Address>.ExecuteCRUDCommand(insertCommand, entity);
            return statusOfExecution;
        }

        public async Task<DbStatus> Delete(Address entity)
        {
            // Account with specified id doesn't exists
            Address existingAddress = await GetByPrimaryKey(entity);
            if (existingAddress == null)
                return DbStatus.NOT_FOUND;

            DbCommand<Address> deleteCommand = new CompletelyDeleteCommand<Address>();
            DbStatus statusOfExecution = await ServiceHelper<Address>.ExecuteCRUDCommand(deleteCommand, entity);
            return statusOfExecution;
        }

        public async Task<IList<Address>> GetAll()
        {
            return await ServiceHelper<Address>.ExecuteSelectCommand(new SelectAllCommand<Address>());
        }

        public async Task<Address> GetByPrimaryKey(Address entity)
        {
            var list = await ServiceHelper<Address>.ExecuteSelectCommand(new SelectWithPrimaryKeyCommand<Address>(), entity);
            var x = list;
            // If no object is found, return null
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<Address> GetByUniqueIdentifiers(string[] propertyNames, Address entity)
        {
            var list = await ServiceHelper<Address>.ExecuteSelectCommand(new SelectWithAttributeValuesCommand<Address>(propertyNames), entity);
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<IList<Address>> GetRange(int begin, int count)
        {
            return await ServiceHelper<Address>.ExecuteSelectCommand(new SelectWithRangeCommand<Address>(begin, count, "Name"));
        }

        public async Task<int> GetTotalNumberOfItems()
        {
            return Convert.ToInt32(await ServiceHelper<Address>.ExecuteScalarCommand(new CountCommand<Address>()));
        }

        public async Task<DbStatus> Update(Address entity)
        {
            // Address with specified id doesn't exists
            Address existingAddress = await GetByPrimaryKey(entity);
            if (existingAddress == null)
                return DbStatus.NOT_FOUND;

            Address addressWithSameName = await GetByUniqueIdentifiers(new string[] { "Name" }, entity);
            if (addressWithSameName != null && existingAddress.Name != entity.Name)
                return DbStatus.EXISTS;

            DbCommand<Address> updateCommand = new UpdateCommand<Address>();
            DbStatus status = await ServiceHelper<Address>.ExecuteCRUDCommand(updateCommand, entity);
            return status;
        }
    }
}
