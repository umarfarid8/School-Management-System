using Microsoft.Identity.Client;
using School_Management_System.DatabaseAccess.EntityFramework;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.DatabaseAccess.Repository
{
    public class TeacherRepo
    {
        public readonly SchoolDbContext _context = new SchoolDbContext();
        public List<Teacher> GetAllTeachers() => _context.Teachers.ToList();

        public void AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }
        public void UpdateTeacher(Teacher teacher)
        {
            using (var db = new SchoolDbContext())
            {
                db.Teachers.Attach(teacher);
                db.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteTeacher(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
        }
    }
}
