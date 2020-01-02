using Database.Entities;
using MySql.Data.MySqlClient;

namespace Database.Commands
{
    /// <summary>
    /// Abstraction for select command that returns all records from one table: SELECT * FROM table_name
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public class SelectAllCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "SELECT * FROM " + tableName;
        }
    }
}
