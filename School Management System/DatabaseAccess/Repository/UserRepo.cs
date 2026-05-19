using School_Management_System.DatabaseAccess.EntityFramework;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.DatabaseAccess.Repository
{
    public class UserRepo
    {
        public bool RegisterUser(User user)
        {
            using (var db = new SchoolDbContext())
            {
                db.Users.Add(user);
                return db.SaveChanges() > 0;

            }
        }
        public User Authenticate(string email, string password)
        {
            using (var db = new SchoolDbContext())
            {
                return db.Users.FirstOrDefault(u => u.EmailAddress == email && u.Password == password);

            }
        }
    }
}
