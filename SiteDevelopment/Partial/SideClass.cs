using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;

namespace SiteDevelopment.Partial
{
    //[Obsolete]
    //public class SideClass
    //{
    //    public IEnumerable<Streak> HomeGames
    //    {
    //        get { return HomeStreak(); }
    //        private set { }
    //    }
    //    public IEnumerable<Streak> AwayGames
    //    {
    //        get { return AwayStreak(); }
    //        private set { }
    //    }

    //    private IEnumerable<Streak> HomeStreak()
    //    {
    //        List<Streak> homeGames = new List<Streak>();
    //        foreach (var match in MatchesHome)
    //        {
    //            if (match.AwayTeamScore < match.HomeTeamScore)
    //            {
    //                homeGames.Add(Streak.Win);
    //            }
    //            else
    //            {
    //                if (match.Result == TypeOfResult.OT || match.Result == TypeOfResult.SO)
    //                {
    //                    homeGames.Add(Streak.OT);
    //                }
    //                else
    //                {
    //                    homeGames.Add(Streak.Loss);
    //                }
    //            }
    //        }
    //        return homeGames;
    //    }

    //    private IEnumerable<Streak> AwayStreak()
    //    {
    //        List<Streak> awayGames = new List<Streak>();
    //        foreach (var match in MatchesAway)
    //        {
    //            if (match.AwayTeamScore > match.HomeTeamScore)
    //            {
    //                awayGames.Add(Streak.Win);
    //            }
    //            else
    //            {
    //                if (match.Result == TypeOfResult.OT || match.Result == TypeOfResult.SO)
    //                {
    //                    awayGames.Add(Streak.OT);
    //                }
    //                else
    //                {
    //                    awayGames.Add(Streak.Loss);
    //                }
    //            }
    //        }
    //        return awayGames;
    //    }
    //}
}