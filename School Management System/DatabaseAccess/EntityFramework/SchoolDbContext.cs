using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
namespace School_Management_System.DatabaseAccess.EntityFramework
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<ClassRecord> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(@"Server=DESKTOP-279I7PS;Database=SchoolManagementDB;Trusted_Connection=True; TrustServerCertificate=True;");
            }

        }
    }
}
