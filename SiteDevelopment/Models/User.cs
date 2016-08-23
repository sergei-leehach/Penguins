using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }

        //[Required(ErrorMessage = "The nickname is required!")]
        public string Nickname { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "The Email address is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required!")]
        [StringLength(100, ErrorMessage = "The password must contain at least 6 characters!", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Invalid password!")]
        public string Password { get; set; }
                
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

        public string Avatar { get; set; }
        public string City { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date of registration")]
        public DateTime DateOfRegistration { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<News> News { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User()
        {
            News = new HashSet<News>();
            Comments = new HashSet<Comment>();
        }

        public User(DbDataReader reader)
        {
            UserId = Convert.ToInt32(reader["UserId"]);
            FirstName = reader["Firstname"].ToString();
            Surname = reader["Surname"].ToString();
            Nickname = reader["Nickname"].ToString();
            Email = reader["Email"].ToString();
            Password = reader["Password"].ToString();
            RememberMe = Convert.ToBoolean(reader["RememberMe"]);
            Avatar = reader["Avatar"].ToString();
            City = reader["City"].ToString();
            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
            DateOfRegistration = Convert.ToDateTime(reader["DateOfRegistration"]);
            RoleId = Convert.ToInt32(reader["RoleId"]);
        }      
    }   
}