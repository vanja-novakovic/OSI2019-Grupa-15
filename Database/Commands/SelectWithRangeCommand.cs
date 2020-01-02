using Database.Entities;
using MySql.Data.MySqlClient;

namespace Database.Commands
{
    /// <summary>
    /// Abstraction for sql command that selects portion of table rows, from specified offset to some limit
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public class SelectWithRangeCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        private readonly int offset;
        private readonly int limit;
        private readonly string orderByAttribute;

        public SelectWithRangeCommand(int offset, int limit, string orderByAttribute)
        {
            this.orderByAttribute = orderByAttribute;
            this.offset = offset;
            this.limit = limit;
        }


        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "SELECT * FROM " + tableName + " ORDER BY " + orderByAttribute + " LIMIT " + limit + " OFFSET " + offset;
        }
    }
}
