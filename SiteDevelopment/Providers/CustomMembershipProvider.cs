using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;
using WebGrease.Css.Ast.Selectors;

namespace SiteDevelopment.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private UserRepository _db;

        public CustomMembershipProvider()
        {
            _db = new UserRepository(DbQuery.ConnectionString);
        }

        public override bool ValidateUser(string email, string password)
        {
            bool isValid = false;
            //try
            //{
                User user = _db.GetUserByEmail(email);

                if (user != null && _db.CheckPassword(user.UserId, password))
                {
                    isValid = true;
                }
            //}
            //catch
            //{
                //isValid = false;
            //}
            return isValid;
        }

        public MembershipUser CreateUser(string nickname, string email, string password)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser == null)
            {
                //try
                //{                 
                    _db.CreateUser(nickname, email, password);
                    membershipUser = GetUser(email, false);
                    return membershipUser;
                //}
                //catch
                //{
                    //return null;
                //}
            }
            return null;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            //try
            //{
            var user = _db.GetUserByEmail(email);

                if (user != null)
                {
                    MembershipUser memberUser = new MembershipUser("CustomMembershipProvider", user.Email, null, null,
                    null, null, false, false, user.DateOfRegistration, DateTime.MinValue, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue);

                    return memberUser;
                }
            //}
            //catch
            //{
                //return null;
            //}
            return null;
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override
            MembershipUser CreateUser(string username, string email, string password, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}