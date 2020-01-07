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
    public class AddressCityService : IAddressCityService
    {
        private ICityService cityService = ServicesFactory.GetInstance().CreateICityService();
        private IAddressService addressService = ServicesFactory.GetInstance().CreateIAddressService();

        public async Task<List<Address>> GetAllInCity(string cityName)
        {
            City city = await cityService.GetByUniqueIdentifiers(new string[] { "Name" }, new City() { Name = cityName });
            DbCommand<AddressCity> command = new SelectWithAttributeValuesCommand<AddressCity>(new string[] { "IdCity" });
            List<AddressCity> addressCities = await ServiceHelper<AddressCity>.ExecuteSelectCommand(command, new AddressCity() { IdCity = city.IdCity });
            List<Address> addresses = new List<Address>();
            foreach (var x in addressCities)
            {
                Address address = await addressService.GetByPrimaryKey(new Address() { IdAddress = x.IdAddress });
                addresses.Add(address);
            }
            return addresses;
        }
    }
}
