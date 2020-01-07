using Core.Common;
using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IEventService : ICrudServiceTemplate<Event>
    {
        Task<List<Event>> GetRangeInOneCity(int offset, int limit, string cityName, string orderByAttribute, string order = "asc");

        Task<int> GetCountInOneCity(string cityName, EventFilter filter, int? idCategory = null);

        Task<List<Event>> GetRangeInOneCityWithFilter(int offset, int limit, string cityName, EventFilter filter, int? idCategory = null, string orderByAttribute = null, string order = "asc");
    }
}
