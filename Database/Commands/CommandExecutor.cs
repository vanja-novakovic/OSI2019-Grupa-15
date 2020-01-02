using Database.Entities;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Database.Commands
{
    /// <summary>
    /// Every MySqlCommand needs to be executed, and this class is used to execute DbCommand, which
    /// is wrapper around MySqlCommand. It allows minimalism of code duplication, because every command
    /// that is created needs to be executes. So we will always use this part of code.
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public static class CommandExecutor<T> where T : IUniquelyIdentifiable, IDbTableAssociate, new()
    {
        /// <summary>
        /// It tries to execute one of the CREATE/READ/UPDATE/DELETE commands and returns success or throws exception
        /// </summary>
        /// <param name="connection">Connection to add in command</param>
        /// <param name="command">Command object previously created (INSERT, UPDATE, COMPLETELYDELETE..)</param>
        /// <param name="entity">Entity to use</param>
        /// <returns>Status of operation</returns>
        public static async Task<DbStatus> TryExecutingCRUDQuery(MySqlConnection connection, DbCommand<T> command, T entity)
        {
            MySqlCommand mysqlcommand = command.GetCommand(connection, (entity as IDbTableAssociate).GetAssociatedDbTableName(), entity);
            await mysqlcommand.ExecuteNonQueryAsync();
            return DbStatus.SUCCESS;
        }

        /// <summary>
        /// It serves to execute select queries, because they return many rows, and when command is executed, 
        /// reader is returned which can be used to iterate through returned rows
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<System.Data.Common.DbDataReader> TryExecutingSelectQueryDataReader(MySqlConnection connection, DbCommand<T> command, T entity = default(T))
        {

            MySqlCommand mySqlCommand = command.GetCommand(connection, (new T() as IDbTableAssociate).GetAssociatedDbTableName(), entity);
            return await mySqlCommand.ExecuteReaderAsync();
        }

        /// <summary>
        /// It returns only one part of row, that is one value
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<object> TryExecutingScalarRead(MySqlConnection connection, DbCommand<T> command, T entity = default(T))
        {

            MySqlCommand mySqlCommand = command.GetCommand(connection, (new T() as IDbTableAssociate).GetAssociatedDbTableName(), entity);
            return await mySqlCommand.ExecuteScalarAsync();
        }
    }
}
