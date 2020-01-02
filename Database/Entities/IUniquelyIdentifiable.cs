using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    /// <summary>
    /// Enables defining primary keys of class
    /// </summary>
    public interface IUniquelyIdentifiable
    {
        string[] GetPrimaryKeyPropertyNames();
    }
}
