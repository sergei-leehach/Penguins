using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private UserRepository _db;

        public CustomRoleProvider()
        {
            _db = new UserRepository(DbQuery.ConnectionString);
        }
        public override string[] GetRolesForUser(string email)
        {
            string[] role = new string[] { };

            //try
            //{
            User user = _db.GetUserByEmail(email);

            if (user != null)
            {
                Role useRole = _db.GetRole(user.RoleId);

                if (useRole != null)
                {
                    role = new string[] { useRole.Name };
                }
            }
            //}
            //catch
            //{
            //    role = new string[] {};
            //}
            return role;
        }

        public override void CreateRole(string roleName)
        {
            Role newRole = new Role { Name = roleName };
            _db.CreateRole(newRole);
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            bool outputResult = false;
            //try
            //{
            User user = _db.GetUserByEmail(email);

            if (user != null)
            {
                Role userRole = _db.GetRole(user.RoleId);

                if (userRole != null && userRole.Name == roleName)
                {
                    outputResult = true;
                }
            }
            //}
            //catch
            //{
            //    outputResult = false;
            //}
            return outputResult;
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}