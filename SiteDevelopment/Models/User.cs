using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;

namespace SiteDevelopment.Models
{
    public class User
    {
        public User()
        {
            News = new HashSet<News>();
            Comments = new HashSet<Comment>(); 
        }

        [Key]
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}