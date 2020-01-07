using Core.Common;
using Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eve.ViewModel
{
    public class EventViewModel
    {
        private static readonly IAddressService addressService = ServicesFactory.GetInstance().CreateIAddressService();

        private int _idAddress;

        public int IdEvent { get; set; }

        public string Name { get; set; }

        public string Organizers { get; set; }

        public string Description { get; set; }

        public int IdCity { get; set; }

        public int IdAddress { get => _idAddress; set { _idAddress = value; SetAddress(value); } }

        public int IdCategory { get; set; }

        public DateTime ScheduledOn { get; set; }

        public int Duration { get; set; }

        public string Address { get; set; }

        public async void SetAddress(int idAddress)
        {
            if (idAddress != 0)
                Address = (await addressService.GetByPrimaryKey(new Database.Entities.Address() { IdAddress = idAddress })).Name;
        }
    }
}
