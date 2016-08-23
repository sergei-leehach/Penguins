using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Division { get; set; }
        public string Name { get; set; }
        public string CityArea { get; set; }
        public string Arena { get; set; }
        public string Logo { get; set; }
        public string Conference { get; set; }
        public string ShortTeamName { get; set; }

        public ICollection<Match> MatchesAway { get; set; }
        public ICollection<Match> MatchesHome { get; set; }
        public ICollection<Standings> Standings { get; set; }

        public Team()
        {
            MatchesAway = new HashSet<Match>();
            MatchesHome = new HashSet<Match>();
            Standings = new HashSet<Standings>();
        }

        public Team(DbDataReader reader)
        {
            TeamId = Convert.ToInt32(reader["TeamId"]);
            Conference = reader["Conference"].ToString();
            Division = reader["Division"].ToString();
            Name = reader["Name"].ToString();
            CityArea = reader["CityArea"].ToString();
            Arena = reader["Arena"].ToString();
            ShortTeamName = reader["ShortTeamName"].ToString();
            Logo = reader["Logo"].ToString();
        }
    }
}