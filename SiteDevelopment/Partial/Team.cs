using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Team
    {    
        public IEnumerable<Streak> AllGames => AllStreak();

        private IEnumerable<Streak> AllStreak()
        {           
            var allGames = new List<Streak>();
            if (MatchesAway == null && MatchesHome == null) return allGames;

            var sortMatches = MatchesAway.ToList().Union(MatchesHome.ToList()).OrderBy(m => m.Date);

            foreach (var match in sortMatches)
            {
                if (match.AwayTeamId == TeamId)
                {
                    if (match.AwayTeamScore > match.HomeTeamScore)
                    {
                        allGames.Add(Streak.Win);
                    }
                    else
                    {
                        if (match.Result == TypeOfResult.OT || match.Result == TypeOfResult.SO)
                        {
                            allGames.Add(Streak.OT);
                        }
                        else
                        {
                            allGames.Add(Streak.Loss);
                        }                      
                    }
                }
                else
                {
                    if (match.AwayTeamScore < match.HomeTeamScore)
                    {
                        allGames.Add(Streak.Win);
                    }
                    else
                    {
                        if (match.Result == TypeOfResult.OT || match.Result == TypeOfResult.SO)
                        {
                            allGames.Add(Streak.OT);
                        }
                        else
                        {
                            allGames.Add(Streak.Loss);
                        }                      
                    }
                }
            }
            return allGames;
        }
    }
}