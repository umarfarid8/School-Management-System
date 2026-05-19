using Microsoft.EntityFrameworkCore;
using School_Management_System.DatabaseAccess.EntityFramework;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.DatabaseAccess.Repository
{
    public class AttendanceRepo
    {
        public void SaveOrUpdateAttendance(List<AttendanceRecord> records)
        {
            using (var context  = new SchoolDbContext())
            {
                foreach(var record in records)
                {
                    var existing = context.AttendanceRecords
                        .FirstOrDefault(a => a.StudentId == record.StudentId && a.Date.Date == record.Date.Date);
                    if ( existing != null)
                    {
                        existing.Status = record.Status;

                    }
                    else
                    {
                        context.AttendanceRecords .Add(record);
                    }
                    

                    
                }
                context.SaveChanges();
            }
        }
        public List<AttendanceRecord> GetHistoryByDate(DateTime date)
        {
            using (var context = new SchoolDbContext())
            {
                return context.AttendanceRecords
                    .Include(a => a.Student)
                    .Where(a => a.Date.Date == date.Date)
                    .ToList();
            }
        }
    }
}
