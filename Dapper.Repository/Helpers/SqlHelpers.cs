using System.Text;
using System.Text.RegularExpressions;

namespace Dapper.Repository.Helpers
{
    class SqlHelpers
    {
        public static string SqlBuilder(string sql, string whereExpression = null, string orderByExpression = null, int? page = null, int? pageSize = null)
        {
            // TODO Add logic to handle paging
            //int index = oldText.IndexOf(System.Environment.NewLine);
            //newText = oldText.Substring(index + System.Environment.NewLine.Length);

            //int index = oldText.IndexOf("\r\n");
            //newText = oldText.Substring(index + 2);
            // sqlQuery.Replace - overload 4

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
