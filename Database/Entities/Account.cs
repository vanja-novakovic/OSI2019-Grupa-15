using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Account : IDbTableAssociate, IUniquelyIdentifiable
    {
        public int IdAccount { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Account";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdAccount" };
        }
    }
}
