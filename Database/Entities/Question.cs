using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Question: IDbTableAssociate, IUniquelyIdentifiable
    {
        public int IdQuestion { get; set; }
        public string Content { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Question";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdQuestion" };
        }
    }
}
