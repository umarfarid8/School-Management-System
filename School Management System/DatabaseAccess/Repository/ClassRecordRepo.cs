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
    internal class ClassRecordRepo
    {
        public IEnumerable<ClassRecord> GetAllClassRecords()
        {
            using (var context = new SchoolDbContext())
            {
                return context.Classes.ToList();
            }
        }
        // logic for adding a class record to the database
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
        // logic for updating a class record in the database    
        public void UpdateClass(ClassRecord classRecord)
        {
            using (var context = new SchoolDbContext())
            {
                context.Classes.Attach(classRecord);
                context.Entry(classRecord).State = EntityState.Modified;
                context.SaveChanges();
            }

        }
        // logic for deleting a class record from the database
        public void DeleteClass(ClassRecord classRecord)
        {
            using (var context = new SchoolDbContext())
            {
               
                context.Classes.Remove(classRecord);
                context.SaveChanges();
            }
        }

       
    }
    }
