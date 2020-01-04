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
    public class EventService : IEventService
    {
        public async Task<DbStatus> Add(Event entity)
        {
            string[] uniqueAtributes = new string[] { "Name", "ScheduledOn", "IdAddress", "IdCity" };
            Event existingEvent = await GetByUniqueIdentifiers(uniqueAtributes, entity);
            if (existingEvent != null)
                return DbStatus.EXISTS;

            DbCommand<Event> insertCommand = new InsertCommand<Event>();
            DbStatus statusOfExecution = await ServiceHelper<Event>.ExecuteCRUDCommand(insertCommand, entity);
            return statusOfExecution;
        }

        public async Task<DbStatus> Delete(Event entity)
        {
            Event existingEvent = await GetByPrimaryKey(entity);
            if (existingEvent == null)
                return DbStatus.NOT_FOUND;

            DbCommand<Event> deleteCommand = new CompletelyDeleteCommand<Event>();
            DbStatus statusOfExecution = await ServiceHelper<Event>.ExecuteCRUDCommand(deleteCommand, entity);
            return statusOfExecution;

        }

        public async Task<IList<Event>> GetAll()
        {
            return await ServiceHelper<Event>.ExecuteSelectCommand(new SelectAllCommand<Event>());
        }

        public async Task<Event> GetByPrimaryKey(Event entity)
        {
            var list = await ServiceHelper<Event>.ExecuteSelectCommand(new SelectWithPrimaryKeyCommand<Event>(), entity);
            return list.Count != 0 ? list[0] : null; 

        }

        public async Task<Event> GetByUniqueIdentifiers(string[] propertyNames, Event entity)
        {
            var list = await ServiceHelper<Event>.ExecuteSelectCommand(new SelectWithAttributeValuesCommand<Event>(propertyNames), entity);
            return list.Count != 0 ? list[0] : null;
        }

        public async Task<IList<Event>> GetRange(int begin, int count)
        {
            return await ServiceHelper<Event>.ExecuteSelectCommand(new SelectWithRangeCommand<Event>(begin, count, "Name"));
        }

        public async Task<int> GetTotalNumberOfItems()
        {
            return Convert.ToInt32(await ServiceHelper<Event>.ExecuteScalarCommand(new CountCommand<Event>()));
        }

        public async Task<DbStatus> Update(Event entity)
        {
            Event existingEvent = await GetByPrimaryKey(entity);
            if (existingEvent == null)
                return DbStatus.NOT_FOUND;

            Event eventWithSameUniqueAtributes = await GetByUniqueIdentifiers(new string[] { "Name", "ScheduledOn", "IdAddress", "IdCity" }, entity);
            if (eventWithSameUniqueAtributes!= null)
                return DbStatus.EXISTS;

            DbCommand<Event> updateCommand = new UpdateCommand<Event>();
            DbStatus status = await ServiceHelper<Event>.ExecuteCRUDCommand(updateCommand, entity);
            return status;
        }
    }
}
