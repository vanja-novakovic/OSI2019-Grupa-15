using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Answer : IUniquelyIdentifiable, IDbTableAssociate
    {
        public int IdAnswer { get; set; }

        public string Content { get; set; }

        public byte True { get; set; }

        public int IdQuestion { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Answer";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdAnswer" };
        }
    }
}
