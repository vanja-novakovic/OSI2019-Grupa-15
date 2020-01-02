using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class City : IUniquelyIdentifiable, IDbTableAssociate
    {
        public int IdCity { get; set; }

        public string Name { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "City";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdCity" };
        }
    }
}
