using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.Repositories
{
    public class UserRepository
    {
        public UserModels GetUser(String id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.User.FirstOrDefault(u => u.Id.Equals(id));
            }
        }
        public List<UserModels> GetAllUsers()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.User.ToList();
            }
        }
        public UserModels SaveUser(UserModels user)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                if (!String.IsNullOrEmpty(user.Id))
                {
                    hatCon.Entry(user).State = EntityState.Modified;
                }
                else
                {
                    hatCon.User.Add(user);
                }
                hatCon.SaveChanges();
                return user;
            }
        }
        public void DeleteUser(string id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var user = hatCon.User.FirstOrDefault(u => u.Id.Equals(id));
                if (user != null)
                {
                    hatCon.User.Remove(user);
                    hatCon.SaveChanges();
                }
            }
        }
    }
}