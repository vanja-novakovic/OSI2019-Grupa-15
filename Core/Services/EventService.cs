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
    public class EventService : IEventService
    {
        public Task<DbStatus> Add(Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Delete(Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Event>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetByPrimaryKey(Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetByUniqueIdentifiers(string[] propertyNames, Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Event>> GetRange(int begin, int count)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalNumberOfItems()
        {
            throw new NotImplementedException();
        }

        public Task<DbStatus> Update(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
