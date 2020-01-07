using Database.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Commands
{
    public class SelectWithRangeAndFilterCommand<T> : DbCommand<T> where T : IUniquelyIdentifiable, IDbTableAssociate
    {
        private readonly int offset;
        private readonly int limit;
        private readonly string order = "asc";
        private readonly string orderByAttribute;
        private readonly string[] filterAttributes;
        private T entity;

        public SelectWithRangeAndFilterCommand(int offset, int limit, string orderByAttribute, string[] attributes, T entity, string order = "asc")
        {
            this.orderByAttribute = orderByAttribute;
            this.offset = offset;
            this.limit = limit;
            this.entity = entity;
            this.order = order;
            this.filterAttributes = attributes;
        }


        protected override void SetCommand(MySqlConnection connection, string tableName, T entity)
        {
            mySqlCommand.Connection = connection;
            string whereExpression = BuildWhereExpression(this.entity, filterAttributes);
            mySqlCommand.CommandText = "SELECT * FROM " + tableName + " " + whereExpression + " ORDER BY " + orderByAttribute + " " + order + " "
                + " LIMIT " + limit + " OFFSET " + offset;
        }
    }
}
