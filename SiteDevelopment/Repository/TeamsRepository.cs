using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public partial class TeamsRepository : IDisposable
    {
        private SiteContext _db;

        public TeamsRepository()
        {
            _db = new SiteContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private Season GetCurrentSeason()
        {
            var season = (from s in _db.Seasons where s.IsCurrent == true select s).ToList();

            if (season.Any())
            {
                return season.LastOrDefault();
            }

            return null;
        }

        public void CreateStandings()
        {
            var currentSeason = GetCurrentSeason();

            foreach(var team in _db.Teams)
            {
                Standings s = new Standings();
                s.Season = currentSeason;
                s.Team = team;

                _db.Standings.Add(s);
            }

            _db.SaveChanges();
        }

        public IEnumerable<Standings> GetDivision()
        {
            var currentSeason = GetCurrentSeason();

            var divisions = from s in _db.Standings orderby s.Team.Division /*orderby s.Points*/ select s;
            
            return divisions.ToList();
        } 
             
        public IEnumerable<Standings> GetLeague()
        {
            var currentSeason = GetCurrentSeason();

            var league = from s in _db.Standings /*where s.Seasons == currentSeason*/ orderby s.Points descending select s;

            return league.ToList();
        }

        public IOrderedEnumerable<KeyValuePair<int, string>> GetTeams()
        {
            var collection = _db.Teams;
            var y = collection.ToDictionary(item => item.TeamId, item => item.Name);
            var s = y.OrderBy(x => x.Value);

            return s;
        }

        public void NhlTableGeneration(Match match)
        {
            _db.Matches.Add(match);

            match.AwayTeam = _db.Teams.Find(match.AwayTeamId);
            match.HomeTeam = _db.Teams.Find(match.HomeTeamId);

            Team winningTeam;
            Team losingTeam;

            int winnersGoals;
            int losersGoals;

            if (match.AwayTeamScore > match.HomeTeamScore)
            {
                winningTeam = match.AwayTeam;
                winnersGoals = match.AwayTeamScore;


                losingTeam = match.HomeTeam;
                losersGoals = match.HomeTeamScore;
            }
            else
            {
                winningTeam = match.HomeTeam;
                winnersGoals = match.HomeTeamScore;

                losingTeam = match.AwayTeam;
                losersGoals = match.AwayTeamScore;
            }
            var currentSeason = GetCurrentSeason();

            Standings winStat = (from x in winningTeam.Standings where x.Season == currentSeason select x).LastOrDefault();
            Standings loseStat = (from x in losingTeam.Standings where x.Season == currentSeason select x).LastOrDefault();

            winStat.GamesPlayed += 1;
            loseStat.GamesPlayed += 1;

            switch (match.Result)
            {
                case TypeOfResult.FT:
                    FulltimeCounter(match, winStat, loseStat, winnersGoals, losersGoals);
                    break;
                case TypeOfResult.OT:
                    OvertimeCounter(match, winStat, loseStat, winnersGoals, losersGoals);
                    break;
                case TypeOfResult.SO:
                    ShootoutCounter(match, winStat, loseStat, winnersGoals, losersGoals);
                    break;
            }

            _db.SaveChanges();
        }

        private void FulltimeCounter(Match match, Standings winner, Standings loser, int winnerGoals, int loserGoals)
        {
            AwayHomeResult(match, winner, loser);

            winner.Wins += 1;
            winner.Points += 2;
            winner.ROW += 1;
            winner.GoalsFor += winnerGoals;
            winner.GoalsAgainst += loserGoals;
            winner.GoalDifferential += winnerGoals - loserGoals;

            LastTenGamesCounter(winner);
            StreakCounter(winner);


            loser.Losses += 1;
            loser.GoalsFor += loserGoals;
            loser.GoalsAgainst += winnerGoals;
            loser.GoalDifferential += loserGoals - winnerGoals;

            LastTenGamesCounter(loser);
            StreakCounter(loser);
        }

        private void OvertimeCounter(Match match, Standings winner, Standings loser, int winnerGoals, int loserGoals)
        {
            AwayHomeResultOT(match, winner, loser);

            winner.Wins += 1;
            winner.Points += 2;
            winner.ROW += 1;
            winner.GoalsFor += winnerGoals;
            winner.GoalsAgainst += loserGoals;
            winner.GoalDifferential += winnerGoals - loserGoals;

            LastTenGamesCounter(winner);
            StreakCounter(winner);


            loser.OT += 1;
            loser.Points += 1;
            loser.GoalsFor += loserGoals;
            loser.GoalsAgainst += winnerGoals;
            loser.GoalDifferential += loserGoals - winnerGoals;

            LastTenGamesCounter(loser);
            StreakCounter(loser);
        }

        private void ShootoutCounter(Match match, Standings winner, Standings loser, int winnerGoals, int loserGoals)
        {
            AwayHomeResultOT(match, winner, loser);

            winner.Wins += 1;
            winner.Points += 2;
            winner.GoalsFor += winnerGoals - 1;
            winner.GoalsAgainst += loserGoals;

            winner.ShootoutWins += 1;

            LastTenGamesCounter(winner);
            StreakCounter(winner);


            loser.OT += 1;
            loser.Points += 1;
            loser.GoalsFor += loserGoals;
            loser.GoalsAgainst += winnerGoals - 1;

            loser.ShootoutLosses += 1;

            LastTenGamesCounter(loser);
            StreakCounter(loser);
        }

        private void AwayHomeResult(Match match, Standings winner, Standings loser)
        {
            if (winner.Team.Name == match.AwayTeam.Name)
            {
                winner.AwayWins += 1;
                loser.HomeLosses += 1;
            }
            else
            {
                winner.HomeWins += 1;
                loser.AwayLosses += 1;
            }
        }

        private void AwayHomeResultOT(Match match, Standings winner, Standings loser)
        {
            if (winner.Team.Name == match.AwayTeam.Name)
            {
                winner.AwayWins += 1;
                loser.HomeOT += 1;
            }
            else
            {
                winner.HomeWins += 1;
                loser.AwayOT += 1;
            }
        }

        [Obsolete]
        private void AwayHomeWinner(Match match, Standings winner)
        {
            if (winner.Team.Name == match.AwayTeam.Name)
            {
                winner.AwayWins += 1;
            }
            else
            {
                winner.HomeWins += 1;
            }
        }

        [Obsolete]
        private void AwayHomeLoser(Match match, Standings loser)
        {
            if (match.Result == TypeOfResult.FT)
            {
                if (loser.Team.Name == match.AwayTeam.Name)
                {
                    loser.AwayLosses += 1;
                }
                else
                {
                    loser.HomeLosses += 1;
                }
            }
            else
            {
                if (loser.Team.Name == match.AwayTeam.Name)
                {
                    loser.AwayOT += 1;
                }
                else
                {
                    loser.HomeOT += 1;
                }
            }            
        }

        private void LastTenGamesCounter(Standings someTeam)
        {
            List<Streak> collection = someTeam.Team.AllGames.ToList();
            collection.Reverse();
            var games = collection.Take(10);

            int wins = 0;
            int losses = 0;
            int OT = 0;

            foreach (var game in games)
            {
                switch (game)
                {
                    case Streak.Win:
                        wins++;
                        break;
                    case Streak.Loss:
                        losses++;
                        break;
                    case Streak.OT:
                        OT++;
                        break;
                }
            }

            someTeam.LastWins = wins;
            someTeam.LastLosses = losses;
            someTeam.LastOT = OT;
        }

        private void StreakCounter(Standings someTeam)
        {
            List<Streak> games = someTeam.Team.AllGames.ToList();
            games.Reverse();

            if (games.Count > 1)
            {
                if (games[1] != games[0])
                {
                    someTeam.Streak = 1;
                }
                else
                {
                    someTeam.Streak += 1;
                }
            }
            else
            {
                someTeam.Streak += 1;
            }
        }
    }
}