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
        private ICityService cityService = ServicesFactory.GetInstance().CreateICityService();

        public async Task<DbStatus> Add(Event entity)
        {
            try
            {
                string[] uniqueAtributes = new string[] { "Name", "ScheduledOn", "IdAddress", "IdCity" };
                Event existingEvent = await GetByUniqueIdentifiers(uniqueAtributes, entity);
                if (existingEvent != null)
                    return DbStatus.EXISTS;

                DbCommand<Event> insertCommand = new InsertCommand<Event>();
                DbStatus statusOfExecution = await ServiceHelper<Event>.ExecuteCRUDCommand(insertCommand, entity);
                return statusOfExecution;
            }
            catch (Exception)
            {
                return DbStatus.DATABASE_ERROR;
            }
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

        public async Task<int> GetCountInOneCity(string cityName, EventFilter filter, int? idCategory = null)
        {
            City city = await cityService.GetByUniqueIdentifiers(new string[] { "Name" }, new City() { Name = cityName });
            Event tmp = new Event()
            {
                IdCity = city.IdCity,
            };
            if (filter == EventFilter.CATEGORY)
            {
                tmp.IdCategory = idCategory.Value;
                DbCommand<Event> scalarCommand = new CountCommand<Event>(new string[] { "IdCity", "IdCategory" }, tmp);
                return Convert.ToInt32(await ServiceHelper<Event>.ExecuteScalarCommand(scalarCommand));
            }
            else
            {
                DbCommand<Event> selectCommand = new SelectWithAttributeValuesCommand<Event>(new string[] { "IdCity" });
                List<Event> events = await ServiceHelper<Event>.ExecuteSelectCommand(selectCommand, tmp);
                if (filter == EventFilter.PRESENT)
                    return events.Where(x => x.ScheduledOn.Date == DateTime.Now.Date).ToList().Count;
                else if (filter == EventFilter.FUTURE)
                    return events.Where(x => x.ScheduledOn > DateTime.Now).ToList().Count;
                else
                    return events.Count;
            }
        }

        public async Task<List<Event>> GetRangeInOneCity(int offset, int limit, string cityName, string orderByAttribute, string order = "asc")
        {
            City city = await cityService.GetByUniqueIdentifiers(new string[] { "Name" }, new City() { Name = cityName });
            Event tmp = new Event()
            {
                IdCity = city.IdCity
            };
            DbCommand<Event> selectCommand = new SelectWithRangeAndFilterCommand<Event>(offset, limit, "Name", new string[] { "IdCity" }, tmp, order);
            return await ServiceHelper<Event>.ExecuteSelectCommand(selectCommand);
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
            try
            {
                Event existingEvent = await GetByPrimaryKey(entity);
                if (existingEvent == null)
                    return DbStatus.NOT_FOUND;

                Event eventWithSameUniqueAtributes = await GetByUniqueIdentifiers(new string[] { "Name", "ScheduledOn", "IdAddress", "IdCity" }, entity);
                if (eventWithSameUniqueAtributes != null)
                    return DbStatus.EXISTS;

                DbCommand<Event> updateCommand = new UpdateCommand<Event>();
                DbStatus status = await ServiceHelper<Event>.ExecuteCRUDCommand(updateCommand, entity);
                return status;
            }
            catch (Exception)
            {
                return DbStatus.DATABASE_ERROR;
            }
        }

        public async Task<List<Event>> GetRangeInOneCityWithFilter(int offset, int limit, string cityName, EventFilter filter, int? idCategory = null, string orderByAttribute = null, string order = "asc")
        {
            City city = await cityService.GetByUniqueIdentifiers(new string[] { "Name" }, new City() { Name = cityName });
            Event tmp = new Event()
            {
                IdCity = city.IdCity,
            };
            if (filter == EventFilter.CATEGORY)
            {
                if (idCategory.HasValue)
                    tmp.IdCategory = idCategory.Value;
                DbCommand<Event> selectCommand = new SelectWithRangeAndFilterCommand<Event>(offset, limit, orderByAttribute ?? "Name", new string[] { "IdCity", "IdCategory" }, tmp, order);
                return await ServiceHelper<Event>.ExecuteSelectCommand(selectCommand);
            }
            else
            {
                DbCommand<Event> selectCommand = new SelectWithAttributeValuesCommand<Event>(new string[] { "IdCity" }, orderByAttribute ?? "Name", order);
                List<Event> events = await ServiceHelper<Event>.ExecuteSelectCommand(selectCommand, tmp);
                if (filter == EventFilter.PRESENT)
                    events = events.Where(x => x.ScheduledOn.Date == DateTime.Now.Date).ToList();
                else if (filter == EventFilter.FUTURE)
                    events = events.Where(x => x.ScheduledOn > DateTime.Now).ToList();
                return events.Skip(offset).Take(limit).ToList(); ;
            }

        }

    }
}
