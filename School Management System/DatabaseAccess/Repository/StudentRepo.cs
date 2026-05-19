using School_Management_System.DatabaseAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
namespace School_Management_System.DatabaseAccess.Repository
{
    public class StudentRepo
    {
       
        //public IEnumerable<Student> GetAllStudentRecords()
        //{
        //    using (var context = new SchoolDbContext())
        //    {
        //        return context.Students.ToList();
        //    }
        //}
        // logic for adding a student to the database
        //public void AddStudent(string firstName, string lastName, string email, string phoneNumber)
        //{


        //    try
        //    {
        //        var newStudent = new Student
        //        {
        //            FirstName = firstName,
        //            LastName = lastName,
        //            Email = email,
        //            PhoneNumber = phoneNumber
        //        };
        //        using (var context = new SchoolDbContext())
        //        {
        //            context.Students.Add(newStudent);
        //            context.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions (e.g., log the error)
        //        Console.WriteLine($"An error occurred while adding a student: {ex.Message}");
        //    }
            
        //}
        public List<Student> GetAllStudentRecords()
        {
            using (var context = new SchoolDbContext())
            {
                return context.Students
                    .Include(s => s.AssignedClass)
                    .Include(s => s.AssignedTeacher)
                    .ToList();

            }      
        }
        public void AddStudentObject(Student student)
        {
            using (var context = new SchoolDbContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        // logic for updating a student record in the database
        public void UpdateStudent(Student student)
        {
            using (var context = new SchoolDbContext())
            {
                context.Students.Attach(student);
                context.Entry(student).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        // logic for deleting a student record from the database
        public void DeleteStudent(Student student)
        {
            using (var context = new SchoolDbContext())
            {

                context.Students.Remove(student);
                context.SaveChanges();
            }
        }
    }
}
