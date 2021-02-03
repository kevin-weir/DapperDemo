using System;

namespace Dapper.Models
{
    class Student
    {
        public long StudentId { get; set; }

        public string FirstName { get; set; }

        public string LasttName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishedDate { get; set; }

        public bool IsActive { get; set; }

        public bool HasGraduated { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
