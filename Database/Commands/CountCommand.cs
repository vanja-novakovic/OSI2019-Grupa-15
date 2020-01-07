using Database.Entities;
using MySql.Data.MySqlClient;

namespace Database.Commands
{
    /// <summary>
    /// Command that is used when it is needed to count records in database table
    /// </summary>
    /// <typeparam name="T">Type of entity - like table name</typeparam>
    public class CountCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        public string WhereExpression = string.Empty;
        public CountCommand() { }
        public CountCommand(string[] whereCondition, T entity)
        {
            WhereExpression = BuildWhereExpression(entity, whereCondition);
        }

        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "SELECT count(*) as Count FROM " + tableName;
            if (WhereExpression != string.Empty)
                mySqlCommand.CommandText += " " + WhereExpression;
        }
    }
}
