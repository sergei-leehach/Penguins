using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace SiteDevelopment.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        public int SeasonId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }   
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public TypeOfResult Result { get; set; }
        public DateTime Date { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }

        public Match()
        {
        }

        public Match(DbDataReader reader)
        {
            var value = reader["MatchId"];
            if (value != DBNull.Value)
            {
                MatchId = Convert.ToInt32(reader["MatchId"]);
                SeasonId = Convert.ToInt32(reader["SeasonId"]);
                HomeTeamId = Convert.ToInt32(reader["HomeTeamId"]);
                AwayTeamId = Convert.ToInt32(reader["AwayTeamId"]);
                HomeTeamScore = Convert.ToInt32(reader["HomeTeamScore"]);
                AwayTeamScore = Convert.ToInt32(reader["AwayTeamScore"]);
                Result = (TypeOfResult)reader["Result"];
                Date = Convert.ToDateTime(reader["Date"]);
            }           
        }
    }
}