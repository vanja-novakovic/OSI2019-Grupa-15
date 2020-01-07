using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IAddressCityService
    {
        Task<List<Address>> GetAllInCity(string cityName);
    }
}
