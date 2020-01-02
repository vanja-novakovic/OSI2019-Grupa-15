using Database.Entities;
using MySql.Data.MySqlClient;
using System.Text;

namespace Database.Commands
{
    public class CompletelyDeleteCommand<T> : DbCommand<T> where T : IDbTableAssociate, IUniquelyIdentifiable
    {
        public static readonly string WHITE_SPACE = " ";
        public static readonly string EQUALS_SIGN = " = ";
        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = BuildCommandText(tableName, entity);
        }

        private string BuildCommandText(string tableName, T entity)
        {
            StringBuilder stringBuilder = new StringBuilder(" DELETE FROM " + tableName);
            string whereExpression = DbCommand<T>.BuildWhereExpression(entity, entity.GetPrimaryKeyPropertyNames());

            return stringBuilder.Append(whereExpression).ToString();
        }
    }
}
