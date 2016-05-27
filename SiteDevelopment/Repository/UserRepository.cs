using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class UserRepository : IDisposable
    {
        private SiteContext _db;

        public UserRepository()
        {
            _db = new SiteContext();
        }

        public User GetUser(int userId)
        {
            var user = _db.Users.Find(userId);
            //var user = (from u in _db.Users where u.UserId == userId select u).SingleOrDefault();
            return user;
        }

        public void CreateUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void EditUser(User user)
        {
            //var editable = GetUser(user.UserId);
            //editable.Avatar = user.Avatar;
            //editable.City = user.City;
            //editable.DateOfBirth = user.DateOfBirth;
            //editable.Email = user.Email;
            //editable.FirstName = user.FirstName;
            //editable.Nickname = user.Nickname;
            //editable.Password = user.Password;
            //editable.Surname = user.Surname;

            _db.Users.Attach(user);
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public bool CheckLogin(User user)
        {
            var isAuthenticated = false;
            var selectedUser = _db.Users.SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password);

            if (selectedUser != null)
            {
                isAuthenticated = true;
                return isAuthenticated;
            }
            return isAuthenticated;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}