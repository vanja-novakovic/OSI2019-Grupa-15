﻿using Database.Entities;
using MySql.Data.MySqlClient;
using System.Text;

namespace Database.Commands
{
    /// <summary>
    ///  Abstraction for select command with filter also: SELECT * FROM table_name WHERE...
    ///  Obviously, it uses WHERE expression, so correspoding method is being called.
    /// </summary>
    /// <typeparam name="T">Type of entity - table name</typeparam>
    public class SelectWithAttributeValuesCommand<T> : DbCommand<T> where T : IDbTableAssociate, IUniquelyIdentifiable
    {
        private readonly string[] attributeNames;
        private readonly string orderByAttribute;
        private string order = "asc";

        /// <summary>
        /// Specify during construction of object of type SelectWithAttributeValuesCommand what to put in WHERE expression
        /// </summary>
        /// <param name="attributeNames">Names of attributes from that table to put in WHERE</param>
        public SelectWithAttributeValuesCommand(string[] attributeNames, string orderByAttribute = null, string order = "asc")
        {
            this.attributeNames = attributeNames;
            this.orderByAttribute = orderByAttribute;
            this.order = order;
        }

        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = BuildCommandText(tableName, entity);
            if (orderByAttribute != null)
                mySqlCommand.CommandText += " ORDER BY " + orderByAttribute + " " + order;
        }

        private string BuildCommandText(string tableName, T entity)
        {
            StringBuilder stringBuilder = new StringBuilder("SELECT * FROM " + tableName);
            string whereExpression = DbCommand<T>.BuildWhereExpression(entity, attributeNames);
            return stringBuilder.Append(whereExpression).ToString();
        }
    }
}
