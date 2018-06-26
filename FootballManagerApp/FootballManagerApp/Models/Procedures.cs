using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FootballManagerApp.Models
{
    public class Procedures
    {
        private static string connectionInfo = "data source=ecRhin.ec.tec.ac.cr\\Estudiantes;initial catalog=FootballManagerDB;persist security info=True;" +
            "user id=anobando;password=anobando;MultipleActiveResultSets=True;App=EntityFramework";

        private static FootballManagerDBEntities db = new FootballManagerDBEntities();
        private static Random rand = new Random();

        // CALENDAR
        public static void GenerateCalendar(short c, short y)
        {
            GenerateSeasons(c, y);
            GenerateMatchdays(c, y);
            GenerateMatches(c, y);
        }
        private static void GenerateSeasons(int c, int y)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Generate_Season", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@competition", SqlDbType.Int).Value = c;
            command.Parameters.Add("@startYear", SqlDbType.Int).Value = y;
            // GET THE VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
        }
        private static void GenerateMatchdays(short c, short y)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Generate_Matchdays", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@competition", SqlDbType.Int).Value = c;
            command.Parameters.Add("@startYear", SqlDbType.Int).Value = y;
            // GET THE VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
        }
        private static void GenerateMatches(int competition, short y)
        {
            List<short> participants = PossibleTeams((short)competition);
            int top = participants.Count;
            int auxiliary = participants.Count;
            int totalMatches = (auxiliary * (auxiliary - 1)) / 2;
            short[] local = new short[totalMatches];
            short[] away = new short[totalMatches];
            int moduleFD = (auxiliary / 2);
            int inverseIndex = auxiliary - 2;
            for (int i = 0; i < totalMatches; i++)
            {
                if (i % moduleFD == 0)
                {
                    if (i % 2 == 0)
                    {
                        local[i] = participants[i % (auxiliary - 1)];
                        away[i] = participants[auxiliary - 1];
                    }
                    else
                    {
                        local[i] = participants[auxiliary - 1];
                        away[i] = participants[i % (auxiliary - 1)];
                    }
                }
                else
                {
                    local[i] = participants[i % (auxiliary - 1)];
                    away[i] = participants[inverseIndex--];
                    if (inverseIndex < 0)
                        inverseIndex = auxiliary - 2;
                }
            }
            List<Match> matches = new List<Match>();
            int matchday = 1;
            int multiple = 0;
            for (int i = 0; i < totalMatches; i++)
            {
                if (multiple == top / 2)
                {
                    multiple = 0;
                    matchday++;
                }
                multiple++;
                matches.Add(new Match(local[i], away[i], 0, 0, matchday, y, competition));
            }

            for (int i = 0; i < totalMatches; i++)
            {
                if (multiple == top / 2)
                {
                    multiple = 0;
                    matchday++;
                }
                multiple++;
                matches.Add(new Match(away[i], local[i], 0, 0, matchday, y, competition));
            }
            foreach (var match in matches)
                db.Matches.Add(match);
            db.SaveChanges();
        }
        private static List<short> PossibleTeams(short compID)
        {
            List<short> teams = new List<short>();
            foreach (Team team in db.Teams.ToList())
            {
                if (team.association.Equals(db.Competitions.Find(compID).association))
                {
                    teams.Add((short)team.iD);
                }
            }
            return teams;
        }

        // SIMULATE COMPETITION
        public static void Simulate(int c, int y)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Simulate_Tournament", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@competition", SqlDbType.Int).Value = c;
            command.Parameters.Add("@startYear", SqlDbType.Int).Value = y;
            // GET THE VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
        }

        // QUERY 1: GENERAL TABLE
        public static List<General_Table_Result> General_Table(int c, int y)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("General_Table", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@competition", SqlDbType.Int).Value = c;
            command.Parameters.Add("@year", SqlDbType.Int).Value = y;
            // GET THE VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            // GET THE RESULT
            int i = 0;
            List<General_Table_Result> table = new List<General_Table_Result>();
            foreach (DataRow row in dataset.Rows)
            {
                string team = db.Teams.Find(int.Parse(row["team"].ToString())).completeName;
                int won = int.Parse(row["won"].ToString());
                int lost = int.Parse(row["lost"].ToString());
                int drawn = int.Parse(row["drawn"].ToString());
                int gFavor = int.Parse(row["gFavor"].ToString());
                int gAgainst = int.Parse(row["gAgainst"].ToString());
                int points = int.Parse(row["points"].ToString());
                General_Table_Result teamData = new General_Table_Result(team, won, lost, drawn, gFavor, gAgainst, points, ++i);
                table.Add(teamData);
            }
            return table;
        }

        // QUERY 2: COACH
        public static Historic_Coach_Result Coaches(int id)
        {
            return new Historic_Coach_Result(db.Coaches.Find(id), teamsCoaches(id));
        }
        private static List<Coach_Teams_Result> teamsCoaches(int id)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Coach_Teams", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@coach", SqlDbType.Int).Value = id;
            // GET VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            // HISTORIC OF TEAMS
            List<Coach_Teams_Result> teams = new List<Coach_Teams_Result>();
            foreach (DataRow row in dataset.Rows)
            {
                string teamAbbreviaton = row["abbreviation"].ToString();
                DateTime signDate = DateTime.Parse(row["signDate"].ToString());
                DateTime breakUpDate = DateTime.Parse(row["signDate"].ToString());
                Coach_Teams_Result team = new Coach_Teams_Result(teamAbbreviaton, signDate, breakUpDate);
                teams.Add(team);
            }
            return teams;
        }

        // QUERY 3: HISTORICS FOR A PLAYER
        public static Historic_Player_Result Historic_Player(short id)
        {
            Player player = db.Players.Find(id);
            List<Player_Teams_Result> teams = teamsResult(id);
            List<Player_Scores_Result> scores = scoresResult(id);
            return new Historic_Player_Result(player, teams, scores);
        }
        private static List<Player_Teams_Result> teamsResult(short id)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Player_Teams", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@player", SqlDbType.Int).Value = id;
            // GET VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            // HISTORIC OF TEAMS
            List<Player_Teams_Result> teams = new List<Player_Teams_Result>();
            foreach (DataRow row in dataset.Rows)
            {
                string teamAbbreviaton = row["abbreviation"].ToString();
                DateTime signDate = DateTime.Parse(row["signDate"].ToString());
                DateTime breakUpDate = DateTime.Parse(row["signDate"].ToString());
                Player_Teams_Result team = new Player_Teams_Result(teamAbbreviaton, signDate, breakUpDate);
                teams.Add(team);
            }
            return teams;
        }
        private static List<Player_Scores_Result> scoresResult(short id)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Player_Scores", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@player", SqlDbType.Int).Value = id;
            // GET VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            // HISTORIC OF SCORES
            List<Player_Scores_Result> scores = new List<Player_Scores_Result>();
            foreach (DataRow row in dataset.Rows)
            {
                short year = short.Parse(row["startYear"].ToString());
                double avarage = double.Parse(row["avarage"].ToString());
                Player_Scores_Result score = new Player_Scores_Result(year, avarage);
                scores.Add(score);
            }
            return scores;
        }

        // QUERY 4: HISTORICS BETWEEEN TEAMS
        public static HistoricMatchesResult HistoricMatches(int t1, int t2)
        {
            List<MatchesResult> matches = Matches(t1, t2);
            List<TopScorersResult> scorers = Scorers(t1, t2);
            return new HistoricMatchesResult(matches, scorers);
        }
        private static List<MatchesResult> Matches(int t1, int t2)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Matches_Between", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@team_1", SqlDbType.Int).Value = t1;
            command.Parameters.Add("@team_2", SqlDbType.Int).Value = t2;
            // GET VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            // HISTORIC OF TEAMS
            List<MatchesResult> matches = new List<MatchesResult>();
            foreach (DataRow row in dataset.Rows)
            {
                string homeAbreviaton = db.Teams.Find(int.Parse(row["home"].ToString())).abbreviation;
                string awayAbreviaton = db.Teams.Find(int.Parse(row["away"].ToString())).abbreviation;
                int goalsHome = int.Parse(row["goalsHome"].ToString());
                int goalsAway = int.Parse(row["goalsAway"].ToString());
                int matchday = int.Parse(row["matchday"].ToString());
                int year = int.Parse(row["startYear"].ToString());
                matches.Add(new MatchesResult(homeAbreviaton, awayAbreviaton, goalsHome, goalsAway, matchday, year));
            }
            return matches;
        }
        private static List<TopScorersResult> Scorers(int t1, int t2)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("TopScorers", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@team_1", SqlDbType.Int).Value = t1;
            command.Parameters.Add("@team_2", SqlDbType.Int).Value = t2;
            // GET VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            // HISTORIC OF TEAMS
            List<TopScorersResult> scorers = new List<TopScorersResult>();
            foreach (DataRow row in dataset.Rows)
            {
                string name = row["firstName"].ToString() + "  " + row["lastName"].ToString();
                int goals = int.Parse(row["total"].ToString());
                scorers.Add(new TopScorersResult(name, goals));
            }
            return scorers;
        }

        // QUERY 5: REFEREES
        /**
         * Entradas: Torneo y temporada.
         * Salidas: Lista con una entidad precisa para la consulta.  Con el nombre de los arbitros y
         * promedio de calificaciones obtenidas en el torneo.
         */
        public static List<Referees_Competition_Result> Referees_Competition(int c, int y)
        {
            SqlConnection connection = new SqlConnection(connectionInfo);
            SqlCommand command = new SqlCommand("Referees_Competition", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // PARAMETERS
            command.Parameters.Add("@competition", SqlDbType.Int).Value = c;
            command.Parameters.Add("@year", SqlDbType.Int).Value = y;
            // GET THE VALUES FROM THE QUERY
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataset = new DataTable();
            adapter.Fill(dataset);
            List<Referees_Competition_Result> referees = new List<Referees_Competition_Result>();
            // GET THE RESULT
            foreach (DataRow row in dataset.Rows)
            {
                string name = row["name"].ToString(); // Name of the Referee
                double a = double.Parse(row["Avarage"].ToString()); // Avarage score in the competition
                Referees_Competition_Result referee = new Referees_Competition_Result(name, a);
                referees.Add(referee);
            }
            connection.Close();
            return referees;
        }
    }
}
