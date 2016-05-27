using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public virtual Season Season { get; set; }
        public virtual Team Team { get; set; }
    }
}