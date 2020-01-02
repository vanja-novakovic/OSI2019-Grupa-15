using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    /// <summary>
    /// Connects entity class with its table name
    /// </summary>
    public interface IDbTableAssociate
    {
        string GetAssociatedDbTableName();
    }
}
