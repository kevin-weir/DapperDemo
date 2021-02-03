using System;

namespace Dapper.Models
{
    class Course
    {
        public long CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
