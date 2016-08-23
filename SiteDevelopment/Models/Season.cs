using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Season
    {      
        [Key]
        public int SeasonId { get; set; }
        public string Title { get; set; }
        public bool IsCurrent { get; set; }

        public ICollection<Standings> Standings { get; set; }
        public ICollection<Match> Matches { get; set; }

        public Season()
        {
            Standings = new HashSet<Standings>();
            Matches = new HashSet<Match>();
        }

        public Season(DbDataReader reader)
        {
            SeasonId = Convert.ToInt32(reader["SeasonId"]);
            Title = reader["Title"].ToString();
            IsCurrent = Convert.ToBoolean(reader["IsCurrent"]);
        }
    }
}