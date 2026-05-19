using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.DatabaseAccess.EntityFramework.Entities
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
