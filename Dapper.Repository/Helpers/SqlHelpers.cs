using System.Text;
using System.Text.RegularExpressions;

namespace Dapper.Repository.Helpers
{
    class SqlHelpers
    {
        public static string SqlBuilder(string sql, string whereExpression = null, string orderByExpression = null, int? page = null, int? pageSize = null)
        {
            // Lets start by removing leading spaces for each line in the sql expression
            sql = Regex.Replace(sql, @"^[\s-[\r\n]]+", "", RegexOptions.Multiline);

            var sqlQuery = new StringBuilder(sql, 2000);

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

            // Only enable paging if we have values for orderByExpression, page and pageSize
            if (orderByExpression is not null && page is not null && pageSize is not null)
            {
                sqlQuery.AppendLine();
                sqlQuery.AppendLine("OFFSET @Offset ROWS");
                sqlQuery.AppendLine("FETCH NEXT @PageSize ROWS ONLY;");
                sqlQuery.AppendLine();

                // Now we are ready to build the COUNT(*) statement
                sqlQuery.AppendLine("SELECT COUNT(*)");
                sqlQuery.Append(sql.Substring(sql.IndexOf("FROM", System.StringComparison.OrdinalIgnoreCase)));

                if (whereExpression is not null)
                {
                    sqlQuery.AppendLine();
                    sqlQuery.Append("WHERE ");
                    sqlQuery.Append(whereExpression);
                }

                sqlQuery.Append(';');
            }
            else
            {
                sqlQuery.Append(';');
            }

            return sqlQuery.ToString();
        }
    }
}
