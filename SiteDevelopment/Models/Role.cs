using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }

        public Role(DbDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            Name = reader["Name"].ToString();
        }
    }
}