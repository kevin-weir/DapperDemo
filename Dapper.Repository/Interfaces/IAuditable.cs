using System;

namespace Dapper.Repository.Interfaces
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime? CreatedDateTime { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedDateTime { get; set; }
    }
}
