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
        public Task<DbStatus> Add(Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Delete(Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Address>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Address> GetByPrimaryKey(Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<Address> GetByUniqueIdentifiers(string[] propertyNames, Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Address>> GetRange(int begin, int count)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalNumberOfItems()
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Update(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
