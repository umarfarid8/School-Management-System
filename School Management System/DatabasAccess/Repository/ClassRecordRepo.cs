using Microsoft.EntityFrameworkCore;
using School_Management_System.DatabasAccess.EntityFramework;
using School_Management_System.DatabasAccess.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.DatabasAccess.Repository
{
    internal class ClassRecordRepo
    {
        public IEnumerable<ClassRecord> GetAllClassRecords()
        {
            using (var context = new SchoolDbContext())
            {
                return context.Classes.ToList();
            }
        }
        public void AddClassRecord(string className, string classRoom)
        {
            using (var context = new EntityFramework.SchoolDbContext())
            {
                var newClassRecord = new EntityFramework.Entities.ClassRecord
                {
                    ClassName = className,
                    ClassRoom = classRoom
                };
                context.Classes.Add(newClassRecord);
                context.SaveChanges();
            }
        }
    }
}
