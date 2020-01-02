using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        public async Task<DbStatus> Add(Account entity)
        {

            // Account with same username must not exist in database
            string[] uniqueAttributes = new string[] { "Username" };
            Account existingAccount = await GetByUniqueIdentifiers(uniqueAttributes, entity);
            if (existingAccount != null)
                return DbStatus.EXISTS;
            // Execute insert command in table Account
            DbCommand<Account> insertCommand = new InsertCommand<Account>();
            DbStatus statusOfExecution = await ServiceHelper<Account>.ExecuteCRUDCommand(insertCommand, entity);
            return statusOfExecution;
        }

        public async Task<DbStatus> Delete(Account entity)
        {
            // Account with specified id doesn't exists
            Account existingAccount = await GetByPrimaryKey(entity);
            if (existingAccount == null)
                return DbStatus.NOT_FOUND;

            DbCommand<Account> deleteCommand = new CompletelyDeleteCommand<Account>();
            DbStatus statusOfExecution = await ServiceHelper<Account>.ExecuteCRUDCommand(deleteCommand, entity);
            return statusOfExecution;
        }

        public async Task<IList<Account>> GetAll()
        {
            return await ServiceHelper<Account>.ExecuteSelectCommand(new SelectAllCommand<Account>());
        }

        public async Task<Account> GetByPrimaryKey(Account entity)
        {
            var list = await ServiceHelper<Account>.ExecuteSelectCommand(new SelectWithPrimaryKeyCommand<Account>(), entity);
            // If no object is found, return null
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<Account> GetByUniqueIdentifiers(string[] propertyNames, Account entity)
        {
            var list = await ServiceHelper<Account>.ExecuteSelectCommand(new SelectWithAttributeValuesCommand<Account>(propertyNames), entity);
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<IList<Account>> GetRange(int begin, int count)
        {
            return await ServiceHelper<Account>.ExecuteSelectCommand(new SelectWithRangeCommand<Account>(begin, count, "Username"));
        }

        public async Task<int> GetTotalNumberOfItems()
        {
            return Convert.ToInt32(await ServiceHelper<Account>.ExecuteScalarCommand(new CountCommand<Account>()));
        }

        public async Task<DbStatus> Update(Account entity)
        {
            // Account with specified id doesn't exists
            Account existingAccount = await GetByPrimaryKey(entity);
            if (existingAccount == null)
                return DbStatus.NOT_FOUND;

            Account accountWithSameUsername = await GetByUniqueIdentifiers(new string[] { "Username" }, entity);
            if (accountWithSameUsername != null && existingAccount.Username != entity.Username)
                return DbStatus.EXISTS;

            DbCommand<Account> updateCommand = new UpdateCommand<Account>();
            DbStatus status = await ServiceHelper<Account>.ExecuteCRUDCommand(updateCommand, entity);
            return status;
        }
    }
}
