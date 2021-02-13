using System;
using System.ComponentModel.DataAnnotations;

namespace Dapper.API.Helpers
{
    public class PagingParameters
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
    }
}
