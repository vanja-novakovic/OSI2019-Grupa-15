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
        public Task<DbStatus> Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByPrimaryKey(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByUniqueIdentifiers(string[] propertyNames, Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetRange(int begin, int count)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalNumberOfItems()
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
