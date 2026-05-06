using School_Management_System.DatabaseAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
namespace School_Management_System.DatabaseAccess.Repository
{
    public class StudentRepo
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();
        public IEnumerable<Student> GetAllStudentRecords() {
            using (var context = new SchoolDbContext())
            {
                return context.Students.ToList();
            }
        }

        public void AddStudent (string firstName, string lastName, string email, string phoneNumber)
        {
            try { 
                var newStudent = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber
                };
                using (var context = new SchoolDbContext())
                {
                    context.Students.Add(newStudent);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"An error occurred while adding a student: {ex.Message}");
            }
            
        }
        public List<Student> GetAllStudentsRecords()
        {

            {
                return _context.Students.ToList();
            }
        }
        }
}
