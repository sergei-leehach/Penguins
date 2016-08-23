using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class TeamsRepository : BaseRepository
    {
        public TeamsRepository(string connectionString) : base(connectionString)
        {
        }
        
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();

            var sqlQuery = "Select * from Teams;";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);

            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var team = new Team(reader);
                teams.Add(team);
            }
            command.Connection.Close();
            return teams;
        }

        public Team GetTeamById(int id)
        {
            //Select * from Teams join Standings on Standings.TeamId = Teams.TeamId join Matches on Matches.SeasonId = Standings.SeasonId where Teams.TeamId  = 8 AND (Matches.HomeTeamId = 8 OR Matches.AwayTeamId = 8)
            var team = new Team();
            var s = new Standings(); 
            var standings = new List<Standings>();
            var matches = new List<Match>();
            var sqlQuery = $"Select * from Teams left join Standings on Standings.TeamId = Teams.TeamId left join Matches on Matches.SeasonId = Standings.SeasonId left join Seasons on Seasons.SeasonId = Standings.SeasonId where Teams.TeamId  = {id}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();            

            while (reader.Read())
            {
                if (team.TeamId == 0)
                {
                    team = new Team(reader);
                }
                if (s.StandingsId != Convert.ToInt32(reader["StandingsId"]))
                {
                    s = new Standings(reader) { Season = new Season(reader) };
                    standings.Add(s);
                }                           
                var match = new Match(reader);
                matches.Add(match);
            }
            command.Connection.Close();
            team.Standings = standings;
            team.MatchesHome = matches.Where(x => x.HomeTeamId == team.TeamId).ToList();
            team.MatchesAway = matches.Where(x => x.AwayTeamId == team.TeamId).ToList();
            return team;
        }

        private Season GetCurrentSeason()
        {                                   
            var sqlQuery = "Select * from Seasons where IsCurrent = 1;";
            var season = GetSeason(sqlQuery);
            return season;
        }

        private Season GetSeasonById(int id)
        {
            var sqlQuery = $"Select * from Seasons where SeasonId = {id};";
            var season = GetSeason(sqlQuery);
            return season;
        }

        private Season GetSeason(string sqlQuery)
        {
            var season = new Season();
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                season = new Season(reader);
            }
            command.Connection.Close();
            return season;
        }

        private string StandingsQuery(Standings standings)
        {
            var sqlQuery =
                $"Update Standings Set GamesPlayed = {standings.GamesPlayed}, Wins = {standings.Wins}, Losses = {standings.Losses}, OT = {standings.OT}, Points = {standings.Points}, ROW = {standings.ROW}, GoalsFor = {standings.GoalsFor}, GoalsAgainst = {standings.GoalsAgainst}, GoalDifferential = {standings.GoalDifferential}, HomeWins = {standings.HomeWins}, HomeLosses = {standings.HomeLosses}, HomeOT = {standings.HomeOT}, AwayWins = {standings.AwayWins}, AwayLosses = {standings.AwayLosses}, AwayOT = {standings.AwayOT}, ShootoutWins = {standings.ShootoutWins}, ShootoutLosses = {standings.ShootoutLosses}, LastWins = {standings.LastWins}, LastLosses = {standings.LastLosses}, LastOT = {standings.LastOT}, Streak = {standings.Streak}, TypeOfResult = '{standings.TypeOfResult}' where StandingsId = {standings.StandingsId};";

            return sqlQuery;
        }

        //var sqlQuery =
        //        $"Insert into Standings (TeamId, SeasonId, GamesPlayed, Wins, Losses, OT, Points, ROW, GoalsFor, GoalsAgainst, GoalDifferential, HomeWins, HomeLosses, HomeOT, AwayWins, AwayLosses, AwayOT, ShootoutWins, ShootoutLosses, LastWins, LastLosses, LastOT, Streak) Values ({standings.TeamId}, {standings.SeasonId}, {standings.GamesPlayed}, {standings.Wins}, {standings.Losses}, {standings.OT}, {standings.Points}, {standings.ROW}, {standings.GoalsFor}, {standings.GoalsAgainst}, {standings.GoalDifferential}, {standings.HomeWins}, {standings.HomeLosses}, {standings.HomeOT}, {standings.AwayWins}, {standings.AwayLosses}, {standings.AwayOT}, {standings.ShootoutWins}, {standings.ShootoutLosses}, {standings.LastWins}, {standings.LastLosses}, {standings.LastOT}, {standings.Streak})";
        public List<Standings> GetStandingsByTeamId(int id)
        {
            var standings = new List<Standings>();
            var sqlQuery = $"Select * from Standings where TeamId = {id};";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var s = new Standings(reader);
                standings.Add(s);
            }
            command.Connection.Close();
            return standings;
        }

        public void CreateStandings()
        {
            var currentSeason = GetCurrentSeason();
            var teams = GetTeams();
            var sqlQuery = string.Empty;
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);

            foreach (var team in teams)
            {
                command.CommandText = $"Insert into Standings (TeamId, SeasonId) Values ('{team.TeamId}', '{currentSeason.SeasonId}');";               
                DbQuery.ExecuteCommand(command);
            }
        }

        public List<Match> GetMatches(string query)
        {
            var matches = new List<Match>();
            var sqlQuery = query;
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);

            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var match = new Match
                {
                    MatchId = Convert.ToInt32(reader["MatchId"]),
                    HomeTeamId = Convert.ToInt32(reader["HomeTeamId"]),
                    AwayTeamId = Convert.ToInt32(reader["AwayTeamId"]),
                    HomeTeamScore = Convert.ToInt32(reader["HomeTeamScore"]),
                    AwayTeamScore = Convert.ToInt32(reader["AwayTeamScore"]),
                    Result = (TypeOfResult) reader["Result"],
                    Date = Convert.ToDateTime(reader["Date"])
                };
                matches.Add(match);
            }
            command.Connection.Close();
            return matches;
        } 

        

        //public IEnumerable<Standings> GetDivision()
        //{
        //    var currentSeason = GetCurrentSeason();

        //    var divisions = from s in _db.Standings orderby s.Team.Division /*orderby s.Points*/ select s;

        //    return divisions.ToList();
        //}

        public List<Standings> GetLeague()
        {
            var league = new List<Standings>();
            var currentSeason = GetCurrentSeason();
            var sqlQuery = $"Select * from Standings join Teams on Teams.TeamId = Standings.TeamId where SeasonId = {currentSeason.SeasonId};";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);

            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var team = new Team(reader);
                var standings = new Standings(reader) { Team = team };
                league.Add(standings);
            }
            //var league = from s in _db.Standings /*where s.Seasons == currentSeason*/ orderby s.Points descending select s;
            return league.ToList();
        }

        public void NhlTableGeneration(Match match)
        {
            match.AwayTeam = GetTeamById(match.AwayTeamId);
            match.HomeTeam = GetTeamById(match.HomeTeamId);

            match.SeasonId = GetCurrentSeason().SeasonId;
            var sqlQuery = $"Insert into Matches (SeasonId, HomeTeamId, AwayTeamId, HomeTeamScore, AwayTeamScore, Result, Date) Values ({match.SeasonId}, {match.HomeTeamId}, {match.AwayTeamId}, {match.HomeTeamScore}, {match.AwayTeamScore}, {(int)match.Result}, '{match.Date}')";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            DbQuery.ExecuteCommand(command);                  

            Team winningTeam;
            Team losingTeam;

            int winnersGoals;
            int losersGoals;

            if (match.AwayTeamScore > match.HomeTeamScore)
            {               
                winningTeam = match.AwayTeam;
                winningTeam.MatchesAway.Add(match);
                winnersGoals = match.AwayTeamScore;

                losingTeam = match.HomeTeam;
                losingTeam.MatchesHome.Add(match);
                losersGoals = match.HomeTeamScore;
            }
            else
            {              
                winningTeam = match.HomeTeam;
                winningTeam.MatchesHome.Add(match);
                winnersGoals = match.HomeTeamScore;

                losingTeam = match.AwayTeam;
                losingTeam.MatchesAway.Add(match);
                losersGoals = match.AwayTeamScore;
            }

            var currentSeason = GetCurrentSeason();
            //if (currentSeason.Standings.Count == 0)
            //{
            //    CreateStandings();
            //}

            var winStat = winningTeam.Standings.SingleOrDefault(x => x.Season.SeasonId == currentSeason.SeasonId);
            var loseStat = losingTeam.Standings.SingleOrDefault(x => x.Season.SeasonId == currentSeason.SeasonId);
            //var winStat = (from s in GetStandingsByTeamId(winningTeam.TeamId) where s.Season.IsCurrent == true select s).FirstOrDefault();
            //var loseStat = (from s in GetStandingsByTeamId(losingTeam.TeamId) where s.Season.IsCurrent == true select s).FirstOrDefault();

            winStat.Team = winningTeam;
            loseStat.Team = losingTeam;
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
            
            command.CommandText = StandingsQuery(winStat);
            DbQuery.ExecuteCommand(command);
            command.CommandText = StandingsQuery(loseStat);
            DbQuery.ExecuteCommand(command);
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

            switch (games[0])
            {
                case Streak.Win:
                    someTeam.TypeOfResult = "Win";
                    break;
                case Streak.Loss:
                    someTeam.TypeOfResult = "Loss";
                    break;
                case Streak.OT:
                    someTeam.TypeOfResult = "OT";
                    break;             
            }
        }      
    }
}