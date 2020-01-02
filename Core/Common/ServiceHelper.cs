using Database.Commands;
using Database.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Common
{
    /// <summary>
    /// It contains logic that is common for all services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ServiceHelper<T> where T : IUniquelyIdentifiable, IDbTableAssociate, new()
    {
        public async static Task<DbStatus> ExecuteCRUDCommand(DbCommand<T> command, T entity)
        {

            IConnector connector = new Connector();
            MySqlConnection connection = connector.CreateConnection();

            try
            {
                await connector.OpenConnection(connection);
                return await CommandExecutor<T>.TryExecutingCRUDQuery(connection, command, entity);
            }
            catch (MySqlException)
            {
                return DbStatus.DATABASE_ERROR;
            }
            finally
            {
                await connector.CloseConnection(connection);
            }
        }
        public async static Task<List<T>> ExecuteSelectCommand(DbCommand<T> command, T entity = default(T))
        {

            IConnector connector = new Connector();

            List<T> rows = new List<T>();
            MySqlConnection connection = connector.CreateConnection();

            try
            {
                await connector.OpenConnection(connection);
                var reader = await CommandExecutor<T>.TryExecutingSelectQueryDataReader(connection, command, entity);
                while (await reader.ReadAsync())
                {
                    rows.Add(ReadOneObject(reader));
                }
                return rows;
            }

            catch (MySqlException)
            {
                return rows;
            }
            finally
            {
                await connector.CloseConnection(connection);
            }
        }
        public async static Task<object> ExecuteScalarCommand(DbCommand<T> command)
        {

            IConnector connector = new Connector();
            object scalarResult = null;

            MySqlConnection connection = connector.CreateConnection();
            try
            {
                await connector.OpenConnection(connection);
                scalarResult = await CommandExecutor<T>.TryExecutingScalarRead(connection, command);
                return scalarResult;
            }

            catch (MySqlException)
            {
                return scalarResult;
            }
            finally
            {
                await connector.CloseConnection(connection);
            }
        }
        private static T ReadOneObject(System.Data.Common.DbDataReader reader)
        {
            T obj = default(T);

            obj = Activator.CreateInstance<T>();
            var properties = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {

                if (!Equals(reader[prop.Name], DBNull.Value))
                {

                    prop.SetValue(obj, reader[prop.Name], null);
                }

            }
            return obj;
        }

    }
}
