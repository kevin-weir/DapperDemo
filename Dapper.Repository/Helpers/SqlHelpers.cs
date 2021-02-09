using System.Text;
using System.Text.RegularExpressions;

namespace Dapper.Repository.Helpers
{
    class SqlHelpers
    {
        public static string SqlBuilder(string sql, string whereExpression = null, string orderByExpression = null)
        {
            // Lets start by removing the leading spaces for each line in the sql expression
            sql = Regex.Replace(sql, @"^[\s-[\r\n]]+", "", RegexOptions.Multiline);

            var sqlQuery = new StringBuilder(sql, 1000);

            if (whereExpression is not null)
            {
                sqlQuery.AppendLine();
                sqlQuery.Append("WHERE ");
                sqlQuery.Append(whereExpression);
            }

            if (orderByExpression is not null)
            {
                sqlQuery.AppendLine();
                sqlQuery.Append("ORDER BY ");
                sqlQuery.Append(orderByExpression);
            }
            sqlQuery.Append(';');

            return sqlQuery.ToString();
        }
    }
}
