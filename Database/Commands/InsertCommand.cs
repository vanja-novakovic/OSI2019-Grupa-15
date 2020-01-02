using Database.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Database.Commands
{
    /// <summary>
    /// Builds insert command: INSERT INTO table_name (att1_name, att2_name...) VALUES (val1, val2,...)
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public class InsertCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        public static readonly string OPENNING_BRACKET = "(";
        public static readonly string CLOSING_BRACKET = ")";
        public static readonly string WHITE_SPACE = " ";
        public static readonly string COMMA = ",";
        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = BuildCommandText(tableName, entity);
        }
        /// <summary>
        ///Every insert command starts with INSERT INTO table_name.
        ///This method builds that part of command text, and appends command text that varies, that is, 
        ///part of insert command with attribute names and values
        /// </summary>
        /// <param name="tableName">Name of table in which record is inserted</param>
        /// <param name="entity">Entity to insert</param>
        /// <returns>Command text</returns>
        private string BuildCommandText(string tableName, T entity)
        {
            StringBuilder stringBuilder = new StringBuilder("INSERT INTO ");
            stringBuilder.Append(tableName + WHITE_SPACE);
            return stringBuilder.Append(BuildVariableCommandText(tableName, entity)).ToString();

        }

        /// <summary>
        /// It builds this part of insert command: (attribute_name1, attribute_name2...) VALUES (value1,value2...)
        /// </summary>
        /// <param name="tableName">Name of table in which record is inserted</param>
        /// <param name="modelEntity">Entity (record) that is being inserted</param>
        /// <returns>Part of INSERT command text</returns>
        private string BuildVariableCommandText(string tableName, T modelEntity)
        {
            StringBuilder stringBuilderColumnNames = new StringBuilder(WHITE_SPACE + OPENNING_BRACKET + WHITE_SPACE);
            StringBuilder stringBuilderCorespondingValues = new StringBuilder(WHITE_SPACE + OPENNING_BRACKET + WHITE_SPACE);
            Type entityType = modelEntity.GetType();

            // Using reflection get all properties of entity that is being inserted, because
            // it corresponds with attributes of table with tableName in database
            PropertyInfo[] propertiesInfo = entityType.GetProperties();

            // Take all properties, accept for primary key, because it is autoincrement and doesn't need to be inserted
            propertiesInfo = propertiesInfo.Where(x => x.Name.CompareTo(modelEntity.GetPrimaryKeyPropertyNames().First()) != 0).ToArray();

            // Take  first n-1 values, because of the comma at the end, and build
            // both parts of command at once
            for (int i = 0; i < propertiesInfo.Length - 1; ++i)
            {
                stringBuilderColumnNames.Append(propertiesInfo[i].Name + COMMA);
                stringBuilderCorespondingValues.Append(AttributeValueHelper.IfStringDoQuotaiton(propertiesInfo[i].GetValue(modelEntity)) + COMMA);
            }
            // Add last piece (attibute and its value)
            stringBuilderColumnNames.Append(propertiesInfo.Last().Name + CLOSING_BRACKET);
            stringBuilderCorespondingValues.Append(AttributeValueHelper.IfStringDoQuotaiton(propertiesInfo.Last().GetValue(modelEntity)) + CLOSING_BRACKET);

            // Put column names and its values together
            stringBuilderColumnNames.Append(" VALUES " + stringBuilderCorespondingValues.ToString());
            return stringBuilderColumnNames.ToString();
        }
    }
}
