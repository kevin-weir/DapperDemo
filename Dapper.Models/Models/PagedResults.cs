using System.Collections.Generic;

namespace Dapper.Domain.Models
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
