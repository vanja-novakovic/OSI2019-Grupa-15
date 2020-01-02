using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Category : IUniquelyIdentifiable, IDbTableAssociate
    {
        public int IdCategory { get; set; }

        public string Name { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Category";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdCategory" };
        }
    }
}
