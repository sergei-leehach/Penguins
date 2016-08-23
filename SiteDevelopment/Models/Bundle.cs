using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class Bundle
    {
        [Key]
        public int Id { get; set; }
        
        public int NewsId { get; set; }
        public int TagId { get; set; }

        public News News { get; set; }
        public Tag Tag { get; set; }

        public Bundle()
        {         
        }

        public Bundle(DbDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            NewsId = Convert.ToInt32(reader["NewsId"]);
            TagId = Convert.ToInt32(reader["TagId"]);
        }
    }
}