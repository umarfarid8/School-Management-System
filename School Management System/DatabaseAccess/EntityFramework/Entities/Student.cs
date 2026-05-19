using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.DatabaseAccess.EntityFramework.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public int? AssignedClassId { get; set; }
        public int? TeacherId { get; set; }

        public virtual ClassRecord? AssignedClass { get; set; }
        public virtual Teacher? AssignedTeacher { get; set; }


        [NotMapped]
        public bool IsPresent { get; set; } = true;

    }
}
