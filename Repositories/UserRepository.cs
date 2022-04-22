using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public List<SelectListItem> UsersToDropDownList()
        { 
            using (var hatCon = new ApplicationDbContext())
            {
                var users = new List<SelectListItem>();
                var noUser = new SelectListItem() { Value = "Ej vald", Text = "Ej vald" };
                users.Add(noUser);

                foreach (var user in GetAllUsers())
                {
                    if (!user.Id.Equals("Ej vald"))
                    {

                        var listitem = new SelectListItem
                        {
                            Value = user.Id.ToString(),
                            Text = user.Name
                        };
                        users.Add(listitem);
                    }
                }
                return users;
            }
        }

        public void addNoUser()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                bool noUserExist = false;
                foreach(var user in GetAllUsers())
                {
                    if(user.Id.Equals("Ej vald"))
                    {
                        noUserExist = true;
                    }
                }

                if (noUserExist == false)
                {
                    UserModels noUser = new UserModels()
                    {
                        Id = "Ej vald",
                        Name = "Ej vald",
                        Password = "Ejvald123!"
                    };
                    hatCon.User.Add(noUser);
                    hatCon.SaveChanges();
                }
            }
        }

        public Dictionary<string, string> DictionaryUsers()
        {
            var users = new Dictionary<string, string>();
            foreach(var user in GetAllUsers())
            {
                users.Add(user.Id.ToString(), user.Name);
            }
            return users;
        }
    }
}