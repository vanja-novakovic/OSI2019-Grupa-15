using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class AddressCity : IUniquelyIdentifiable, IDbTableAssociate
    {
        public int IdCity { get; set; }

        public int IdAddress { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Address_City";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdCity", "IdAddress" };
        }
    }
}
