using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Address : IDbTableAssociate, IUniquelyIdentifiable
    {
        public int IdAddress { get; set; }
        public string Name { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Address";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdAddres" };
        }
    }
}
