using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SiteDevelopment.Models;

namespace SiteDevelopment.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string email)
        {
            string[] role = new string[] {};
            using (SiteContext _db = new SiteContext())
            {
                try
                {
                    User user = _db.Users.SingleOrDefault(x => x.Email == email);

                    if (user != null)
                    {
                        Role useRole = _db.Roles.Find(user.RoleId);

                        if (useRole != null)
                        {
                            role = new string[] {useRole.Name};
                        }
                    }
                }
                catch
                {
                    role = new string[] {};
                }
            }
            return role;
        }

        public override void CreateRole(string roleName)
        {
            Role newRole = new Role() { Name = roleName };
            SiteContext _db = new SiteContext();
            _db.Roles.Add(newRole);
            _db.SaveChanges();
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            bool outputResult = false;
            using (SiteContext _db = new SiteContext())
            {
                try
                {
                    User user = _db.Users.SingleOrDefault(x => x.Email == email);

                    if (user != null)
                    {
                        Role userRole = _db.Roles.Find(user.RoleId);

                        if (userRole != null && userRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
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