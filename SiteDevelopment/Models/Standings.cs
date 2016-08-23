using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Standings
    { 
        [Key]
        public int StandingsId { get; set; }
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OT { get; set; }
        public int Points { get; set; }
        public int ROW { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifferential { get; set; }
        public int HomeWins { get; set; }
        public int HomeLosses { get; set; }
        public int HomeOT { get; set; }
        public int AwayWins { get; set; }
        public int AwayLosses { get; set; }
        public int AwayOT { get; set; }
        public int ShootoutWins { get; set; }
        public int ShootoutLosses { get; set; }
        public int LastWins { get; set; }
        public int LastLosses { get; set; }
        public int LastOT { get; set; }
        public int Streak { get; set; }
        public string TypeOfResult { get; set; }

        public Season Season { get; set; }
        public Team Team { get; set; }

        public Standings()
        {          
        }

        public Standings(DbDataReader reader)
        {
            StandingsId = StandingsSet(reader, "StandingsId");
            TeamId = StandingsSet(reader, "TeamId");
            SeasonId = StandingsSet(reader, "SeasonId");
            GamesPlayed = StandingsSet(reader, "GamesPlayed");
            Wins = StandingsSet(reader, "Wins");
            Losses = StandingsSet(reader, "Losses");
            OT = StandingsSet(reader, "OT");
            Points = StandingsSet(reader, "Points");
            ROW = StandingsSet(reader, "ROW");
            GoalsFor = StandingsSet(reader, "GoalsFor");
            GoalsAgainst = StandingsSet(reader, "GoalsAgainst");
            GoalDifferential = StandingsSet(reader, "GoalDifferential");
            HomeWins = StandingsSet(reader, "HomeWins");
            HomeLosses = StandingsSet(reader, "HomeLosses");
            HomeOT = StandingsSet(reader, "HomeOT");
            AwayWins = StandingsSet(reader, "AwayWins");
            AwayLosses = StandingsSet(reader, "AwayLosses");
            AwayOT = StandingsSet(reader, "AwayOT");
            ShootoutWins = StandingsSet(reader, "ShootoutWins");
            ShootoutLosses = StandingsSet(reader, "ShootoutLosses");
            LastWins = StandingsSet(reader, "LastWins");
            LastLosses = StandingsSet(reader, "LastLosses");
            LastOT = StandingsSet(reader, "LastOT");
            Streak = StandingsSet(reader, "Streak");
            TypeOfResult = reader["TypeOfResult"].ToString();
        }

        private static int StandingsSet(DbDataReader reader, string field)
        {
            var number = Convert.ToInt32(reader[field]);
            return number;
        }
    }
}