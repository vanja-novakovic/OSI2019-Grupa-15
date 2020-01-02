using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Database.Commands
{
    /// <summary>
    /// It creates MySqConnection and returns it. It is used to check if connection is opened, before it closes it.
    /// </summary>
    public class Connector : IConnector
    {

        public MySqlConnection CreateConnection()
        {
            return CreateConnection(Config.Properties.Default.Eve);
        }
        public MySqlConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        public async Task OpenConnection(MySqlConnection connection)
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }
            }
            catch (MySqlException)
            {
            }
        }
        public async Task CloseConnection(MySqlConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                await connection.CloseAsync();
            }
        }
    }
}
