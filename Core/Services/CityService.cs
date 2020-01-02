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
        public Task<DbStatus> Add(City entity)
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Delete(City entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<City>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<City> GetByPrimaryKey(City entity)
        {
            throw new NotImplementedException();
        }

        public Task<City> GetByUniqueIdentifiers(string[] propertyNames, City entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<City>> GetRange(int begin, int count)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalNumberOfItems()
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Update(City entity)
        {
            throw new NotImplementedException();
        }
    }
}
