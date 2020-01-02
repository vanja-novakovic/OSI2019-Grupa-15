using Database.Entities;
using MySql.Data.MySqlClient;
using System.Text;

namespace Database.Commands
{
    /// <summary>
    /// It is used as an abstraction for command that returns only one row from database that has some
    /// value on place of primary key: SELECT * FROM table_name WHERE primary_key_name=primary_key_value.
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public class SelectWithPrimaryKeyCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = BuildCommandText(tableName, entity);
        }
        private string BuildCommandText(string tableName, T entity)
        {
            StringBuilder stringBuilder = new StringBuilder("SELECT * FROM " + tableName);

            /// Send only primary keys to build where expression
            string whereExpression = DbCommand<T>.BuildWhereExpression(entity, entity.GetPrimaryKeyPropertyNames());
            return stringBuilder.Append(whereExpression).ToString();
        }
    }
}
