using Database.Entities;
using MySql.Data.MySqlClient;

namespace Database.Commands
{
    /// <summary>
    /// Class that inherites type of DbCommand so it can be used wherever DbCommand is mentioned, 
    /// but you need to send it command text, it doesn't build it
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public class CustomCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        public override MySqlCommand GetCommand(MySqlConnection connection, string command, T entity)
        {

            SetCommand(connection, command, entity);
            return mySqlCommand;
        }

        protected override void SetCommand(MySqlConnection connection, string command, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = command;
        }
    }
}
