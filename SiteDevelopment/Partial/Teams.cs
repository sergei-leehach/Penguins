using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public partial class Teams
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
            List<Streak> HomeGames = new List<Streak>();
            foreach (var match in MatchesHome)
            {
                if (match.AwayTeamScore < match.HomeTeamScore)
                {
                    HomeGames.Add(Streak.Win);
                }
                else
                {
                    if (match.Result == TypeOfWin.OverTime || match.Result == TypeOfWin.Shootout)
                    {
                        HomeGames.Add(Streak.OT);
                    }
                    HomeGames.Add(Streak.Loss);
                }
            }
            return HomeGames;
        }

        private IEnumerable<Streak> AwayStreak()
        {
            List<Streak> AwayGames = new List<Streak>();
            foreach (var match in MatchesAway)
            {
                if (match.AwayTeamScore > match.HomeTeamScore)
                {
                    AwayGames.Add(Streak.Win);
                }
                else
                {
                    if (match.Result == TypeOfWin.OverTime || match.Result == TypeOfWin.Shootout)
                    {
                        AwayGames.Add(Streak.OT);
                    }
                    AwayGames.Add(Streak.Loss);
                }
            }
            return AwayGames;
        }

        private IEnumerable<Streak> AllStreak()
        {
            List<Streak> AllGames = new List<Streak>();
            var SortMatches = ((from m in MatchesAway select m).Union(from m in MatchesHome select m)).OrderBy(m => m.Date);

            foreach (var match in SortMatches)
            {
                if (match.AwayTeam.Name == Name)
                {
                    if (match.AwayTeamScore > match.HomeTeamScore)
                    {
                        AllGames.Add(Streak.Win);
                    }
                    else
                    {
                        if (match.Result == TypeOfWin.OverTime || match.Result == TypeOfWin.Shootout)
                        {
                            AllGames.Add(Streak.OT);
                        }
                        AllGames.Add(Streak.Loss);
                    }
                }
                else
                {
                    if (match.AwayTeamScore < match.HomeTeamScore)
                    {
                        AllGames.Add(Streak.Win);
                    }
                    else
                    {
                        if (match.Result == TypeOfWin.OverTime || match.Result == TypeOfWin.Shootout)
                        {
                            AllGames.Add(Streak.OT);
                        }
                        AllGames.Add(Streak.Loss);
                    }
                }
            }
            return AllGames;
        }
    }
}