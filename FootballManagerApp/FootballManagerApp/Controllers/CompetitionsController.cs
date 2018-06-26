using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballManagerApp.Models;

namespace FootballManagerApp.Controllers
{
    public class CompetitionsController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: Competitions
        public ActionResult Index()
        {
            var competitions = db.Competitions.Include(c => c.Association1).Include(c => c.Competition_Type);
            return View(competitions.ToList());
        }

        // GET: Competitions/Details/5
        public ActionResult Details(decimal id)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // GET: Competitions/Create
        public ActionResult Create()
        {
            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName");
            ViewBag.c_Type = new SelectList(db.Competition_Type, "type", "description");
            return View();
        }

        /**
         * POST: Competitions/Create
         * To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         * more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost] [ValidateAntiForgeryToken] public ActionResult Create([Bind(Include = "iD,complete_Name,c_Type,association")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                db.Competitions.Add(competition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName", competition.association);
            ViewBag.c_Type = new SelectList(db.Competition_Type, "type", "description", competition.c_Type);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public ActionResult Edit(decimal id)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName", competition.association);
            ViewBag.c_Type = new SelectList(db.Competition_Type, "type", "description", competition.c_Type);
            return View(competition);
        }

        /**
         * POST: Competitions/Edit/5
         * To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         * more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost] [ValidateAntiForgeryToken] public ActionResult Edit([Bind(Include = "iD,complete_Name,c_Type,association")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName", competition.association);
            ViewBag.c_Type = new SelectList(db.Competition_Type, "type", "description", competition.c_Type);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public ActionResult Delete(decimal id)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        public ActionResult Seasons(decimal id)
        {
            List<Season> seasons = new List<Season>();
            foreach (var season in db.Seasons.ToList())
                if (season.Competition1.iD == id)
                    seasons.Add(season);
            ViewBag.Title = db.Competitions.Find(id).complete_Name;
            ViewBag.competition = id;
            return View(seasons);
        }

        public ActionResult Matchdays(decimal c, int y)
        {
            List<Matchday> matchdays = new List<Matchday>();
            foreach (var matchday in db.Matchdays.ToList())
                if (matchday.competition == c && matchday.startYear == y)
                    matchdays.Add(matchday);
            ViewBag.Title = db.Competitions.Find(c).complete_Name + ": " + y.ToString();
            return View(matchdays);

        }

        public ActionResult Competition_Year()
        {
            ViewBag.complete_Name = new SelectList(db.Competitions, "complete_Name","complete_Name");
            return View(db.Competitions.First());
        }

        public ActionResult General_Table_Q(int c, int y)
        {
            List<General_Table_Result> table = Procedures.General_Table(c, y);
            ViewBag.Title = db.Competitions.Find(c).complete_Name + ": " + y.ToString();
            return View("General_Table", table);
        }

        public ActionResult Referees_Q(int c, int y)
        {
            List<Referees_Competition_Result> r = Procedures.Referees_Competition(c, y);
            return View("Referee_Competition", r);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken] public ActionResult DeleteConfirmed(decimal id)
        {
            Competition competition = db.Competitions.Find(id);
            db.Competitions.Remove(competition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete_T(int c, int y)
        {
            Season season = searchSeason(c, y);
            db.Seasons.Remove(season);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult New_Season(short c)
        {
            Procedures.GenerateCalendar(c, lastSeason(c));
            return RedirectToAction("Index");
        }


        // Auxiliares
        private Season searchSeason(int c, int y)
        {
            foreach (var season in db.Seasons.ToList())
                if (season.competition == c && season.startYear == y)
                    return season;
            return null;
        }
        private short lastSeason(int c)
        {
            short last = 0;
            foreach (Season season in db.Seasons.ToList())
                if (season.competition == c && season.startYear > last)
                    last = season.startYear;
            return ++last;
        }

        public ActionResult Simulate(int c, int y)
        {
            Procedures.Simulate(c, y);
            return RedirectToAction("Index");
        }

        public ActionResult AddToLineUp(decimal match, decimal team)
        {
            return RedirectToAction("CreateFixed", "LineUps", new { match = match, team = team });
        }

        public ActionResult EditLineUp(decimal player, decimal match)
        {
            return RedirectToAction("Edit", "LineUps", new { player = player, match = match });
        }

        public ActionResult DeleteLineUp(decimal match, decimal player)
        {
            return RedirectToAction("Delete", "LineUps", new { player = player, match = match });
        }
        
        public ActionResult EditMatch(decimal id)
        {
            return RedirectToAction("Edit","Matches", new {id = id});
        }         
        
        public ActionResult DetailsMatch(decimal id)
        {
            return RedirectToAction("Details","Matches", new {id = id});
        }            
        
        public ActionResult DeleteMatch(decimal id)
        {
            return RedirectToAction("Delete","Matches", new {id = id});
        }

        public ActionResult LineUp(decimal match, decimal team)
        {
            string query = "SELECT * FROM LineUp l inner join Player_Team p on (l.player = p.player) WHERE l.match = @p0 and p.team = @p1 ";
            IEnumerable<FootballManagerApp.Models.LineUp> lineUp = db.LineUps.SqlQuery(query, match, team).ToList();
            if (lineUp.Count() == 0)
                return AddToLineUp(match, team);
            return View(lineUp);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Historic(int team_1, int team_2)
        {
            HistoricMatchesResult result = Procedures.HistoricMatches(team_1, team_2);
            return View(result);
        }
    }
}
