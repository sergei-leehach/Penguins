using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class InputData
    {
        public TypeOfBoard TypeOfBoard { get; set; }
        public DateTime DateOfAMatch { get; set; }
        public string Location { get; set; }
        public string Arena { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeamShortName { get; set; }
        public string AwayTeamShortName { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamLogo { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string BackgroundImage { get; set; }
        public TypeOfWin TypeOfWin { get; set; }
    }
}