using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Team
    {
        public IEnumerable<Streak> HomeGames
        {
            get { return HomeStreak(); }
            private set { }
        }
        public IEnumerable<Streak> AwayGames
        {
            get { return AwayStreak(); }
            private set { }
        }
        public IEnumerable<Streak> AllGames
        {
            get { return AllStreak(); }
            private set { }
        }

        private IEnumerable<Streak> HomeStreak()
        {
            List<Streak> homeGames = new List<Streak>();
            foreach (var match in MatchesHome)
            {
                if (match.AwayTeamScore < match.HomeTeamScore)
                {
                    homeGames.Add(Streak.Win);
                }
                else
                {
                    if (match.Result == TypeOfResult.OT || match.Result == TypeOfResult.SO)
                    {
                        homeGames.Add(Streak.OT);
                    }
                    else
                    {
                        homeGames.Add(Streak.Loss);
                    }
                }
            }
            return homeGames;
        }

        private IEnumerable<Streak> AwayStreak()
        {
            List<Streak> awayGames = new List<Streak>();
            foreach (var match in MatchesAway)
            {
                if (match.AwayTeamScore > match.HomeTeamScore)
                {
                    awayGames.Add(Streak.Win);
                }
                else
                {
                    if (match.Result == TypeOfResult.OT || match.Result == TypeOfResult.SO)
                    {
                        awayGames.Add(Streak.OT);
                    }
                    else
                    {
                        awayGames.Add(Streak.Loss);
                    }                    
                }
            }
            return awayGames;
        }

        private IEnumerable<Streak> AllStreak()
        {
            List<Streak> allGames = new List<Streak>();
            var sortMatches = ((from m in MatchesAway select m).Union(from m in MatchesHome select m)).OrderBy(m => m.Date);

            foreach (var match in sortMatches)
            {
                if (match.AwayTeam.Name == Name)
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