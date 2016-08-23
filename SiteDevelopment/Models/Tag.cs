using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }        
        public string Title { get; set; }

        public ICollection<Bundle> Bundle { get; set; }

        public Tag()
        {
            Bundle = new HashSet<Bundle>();
        }

        public Tag(DbDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            Title = reader["Title"].ToString();
        }
    }
}