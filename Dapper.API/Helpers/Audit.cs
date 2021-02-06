using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Repository.Interfaces;

namespace Dapper.API.Helpers
{
    //public class Audit<T> where T : IAuditable

    public class Audit<T> where T : IAuditable
    {
        public static T PerformAudit(T entity)
        {
            // TODO After security is added make sure to record user who is creating or modifying records
            if (entity.CreatedDateTime == null)
            {
                entity.CreatedDateTime = DateTime.Now;
                entity.ModifiedDateTime = DateTime.Now;
            }
            else 
            {
                entity.ModifiedDateTime = DateTime.Now;
            }

            return entity;
        }
    }
}
