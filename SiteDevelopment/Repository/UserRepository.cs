using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
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
            return _db.Users.Find(userId);
        }

        public User GetUser(string email)
        {
            return _db.Users.SingleOrDefault(x => x.Email == email);
        }

        public Role GetRole(int? roleId) //Check this method as soon as possible!
        {
            return _db.Roles.Find(roleId);
        }

        public void CreateUser(string nickname, string email, string password)
        {
            User user = new User();
            user.Nickname = nickname;
            user.Email = email;
            user.Password = HashingPassword(password);
            user.DateOfRegistration = DateTime.Now;

            if (GetRole(2) != null)
            {
                user.RoleId = 2;
            }

            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void CreateRole(Role role)
        {
            _db.Roles.Add(role);
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

        public string HashingPassword(string unhashedPassword)
        {
            const string salt = "OOMPH!";
            StringBuilder saltPassword = new StringBuilder();

            for (var i = 0; i < unhashedPassword.Length; i++)
            {
                saltPassword.Append(unhashedPassword[i]);
                if (i < 5)
                {
                    saltPassword.Append(salt[i]);
                }
                if (i == unhashedPassword.Length - 1)
                {
                    saltPassword.Append(salt[salt.Length - 1]);
                }
            }
            return Crypto.SHA256(saltPassword.ToString());
            //Save hashedPassword somewhere that you can retrieve it again.
            //Don't save unhashedPassword! Just let it go.
        }

        public bool CheckPassword(int userId, string unhashedPassword)
        {
            string savedHashedPassword = string.Empty;
            var user = GetUser(userId);
            if (user != null)
            {
                savedHashedPassword = user.Password; //get hashedPassword from where you saved it
            }
            string verification = HashingPassword(unhashedPassword);
            bool isMatch = savedHashedPassword.Equals(verification);

            return isMatch;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}