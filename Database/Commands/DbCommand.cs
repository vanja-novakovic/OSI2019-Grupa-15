using Database.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Text;

namespace Database.Commands
{
    /// <summary>
    /// Abstract class used as abstraction for SQL command expression.
    /// It acts like wrapper around MySqlCommand object, and it initializes it with connection and command text.
    /// Command text used dependes on type of class that inherites this abstract class.
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public abstract class DbCommand<T> where T : IDbTableAssociate, IUniquelyIdentifiable
    {
        /// <summary>
        /// MySqlCommand object used for communicating with MySql database
        /// </summary>
        protected readonly MySqlCommand mySqlCommand = new MySqlCommand();

        private static readonly string WHITE_SPACE = " ";
        private static readonly string EQUALS_SIGN = " = ";

        /// <summary>
        /// This method accepts connection and table name, as well as entity of that type,
        /// and sets the mysqlcommand so it has connection and command text. It can be overriden, but has
        /// default behaviour.
        /// </summary>
        /// <param name="connection">Connection with database</param>
        /// <param name="tableName">Name of table for which command is built</param>
        /// <param name="entity">Entity whose values of properties are used</param>
        /// <returns>Initialized MySqlCommand object</returns>
        public virtual MySqlCommand GetCommand(MySqlConnection connection, string tableName, T entity)
        {
            SetCommand(connection, tableName, entity);
            return mySqlCommand;
        }

        /// <summary>
        /// Must be overriden. It needs to set connection and command text for mySqlCommand
        /// </summary>
        /// <param name="connection">Connection with database to be used</param>
        /// <param name="tableName">Table name</param>
        /// <param name="entity">Entity which values of properites are used</param>
        protected abstract void SetCommand(MySqlConnection connection, string tableName, T entity);

        /// <summary>
        /// Every sql command can have WHERE expression, and it si similar in all it's uses. Like:
        ///     DELETE FROM table_name WHERE some_attribute=some_value and some_other_attribute=some_other_value...
        /// </summary>
        /// <param name="entity">Entity that is used to build WHERE expression</param>
        /// <param name="attributesName">Names of attributes to include in WHERE expression</param>
        /// <returns>Built part of command text, that is, WHERE expression</returns>
        protected static string BuildWhereExpression(T entity, string[] attributesName)
        {
            StringBuilder stringBuilder = new StringBuilder(" WHERE ");
            // Use reflection to get type of entity
            Type entityType = entity.GetType();

            // Take last attribute from those sent, because it doesn't need to have ','sign after it, it is end
            // of where expression
            string lastAttribute = attributesName.Last();
            attributesName = attributesName.TakeWhile(x => x != lastAttribute).ToArray();
            foreach (string key in attributesName)
            {
                // Get values of attributes in enitity and append "AND" after it
                stringBuilder.Append(WHITE_SPACE + key + EQUALS_SIGN + AttributeValueHelper.IfStringDoQuotaiton(entityType.GetProperty(key).GetValue(entity)) + " AND");
            }

            // Add last attribute with no "AND" after
            stringBuilder.Append(WHITE_SPACE + lastAttribute + EQUALS_SIGN + AttributeValueHelper.IfStringDoQuotaiton(entityType.GetProperty(lastAttribute).GetValue(entity)));
            return stringBuilder.ToString();
        }

    }
}
