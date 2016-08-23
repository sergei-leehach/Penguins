using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        private string UserQuery(User user)
        {
            var sqlQuery =
                $"Insert into User (Nickname, Email, Password, DateOfRegistration, RoleId) Values ({user.Nickname}, {user.Email}, {user.Password}, {user.DateOfRegistration}, {user.RoleId})";
            return sqlQuery;
        }

        public User GetUserById(int id)
        {
            var user = new User();
            var sqlQuery = $"Select * from Users where UserId = {id}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                user = new User(reader);
            }
            command.Connection.Close();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = new User();
            var sqlQuery = $"Select * from Users where Email = {email}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                user = new User(reader);
            }
            command.Connection.Close();
            return user;
        }

        public Role GetRole(int? id) //Check this method as soon as possible!
        {
            var role = new Role();
            var sqlQuery = $"Select * from Role where RoleId = {id}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                role.Id = Convert.ToInt32(reader["Id"]);
                role.Name = reader["Name"].ToString();
            }
            command.Connection.Close();
            return role;
        }

        public void CreateUser(string nickname, string email, string password)
        {
            User user = new User
            {
                Nickname = nickname,
                Email = email,
                Password = HashingPassword(password),
                DateOfRegistration = DateTime.Now
            };

            if (GetRole(2) != null)
            {
                user.RoleId = 2;
            }

            var sqlQuery = UserQuery(user);
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            DbQuery.ExecuteCommand(command);
        }

        public void CreateRole(Role role)
        {
            var sqlQuery = $"Insert into Role (Id, Name) Values ({role.Id}, {role.Name}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            DbQuery.ExecuteCommand(command);
        }

        public void EditUser(User user)
        {
            var sqlQuery =
                $"Update User Set FirstName = {user.FirstName}, Surname = {user.Surname}, Nickname = {user.Nickname}, Email = {user.Email}, Password = {user.Password}, RememberMe = {user.RememberMe}, Avatar = {user.Avatar}, City = {user.City}, DateOfBirth = {user.DateOfBirth} Where UserId = {user.UserId}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            DbQuery.ExecuteCommand(command);
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
            var user = GetUserById(userId);
            if (user != null)
            {
                savedHashedPassword = user.Password; //get hashedPassword from where you saved it
            }
            string verification = HashingPassword(unhashedPassword);
            bool isMatch = savedHashedPassword.Equals(verification);

            return isMatch;
        }
    }
}