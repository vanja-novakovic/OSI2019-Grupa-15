using MySql.Data.MySqlClient;

namespace Database.Commands
{
    public class DbParameter
    {
        protected readonly MySqlParameter mySqlParameter;
        public DbParameter(string name, MySqlDbType type, object value)
        {
            mySqlParameter = new MySqlParameter(name, type);
            mySqlParameter.Value = value;
        }
        public MySqlParameter GetParameter()
        {
            return mySqlParameter;
        }
    }
}
