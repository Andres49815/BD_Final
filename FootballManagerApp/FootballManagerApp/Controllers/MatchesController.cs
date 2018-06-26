using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballManagerApp.Models;

namespace FootballManagerApp.Controllers
{
    public class MatchesController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: Matches
        public ActionResult Index()
        {
            var matches = db.Matches.Include(m => m.Matchday1).Include(m => m.Team).Include(m => m.Team1);
            return View(matches.ToList());
        }

        // GET: Matches/Details/5
        public ActionResult Details(decimal id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            ViewBag.matchday = new SelectList(db.Matchdays, "matchday1", "matchday1");
            ViewBag.away = new SelectList(db.Teams, "iD", "abbreviation");
            ViewBag.home = new SelectList(db.Teams, "iD", "abbreviation");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,home,away,goalsHome,goalsAway,matchday,startYear,competition")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
                return BackToMatches(match.competition, match.startYear);
            }

            ViewBag.matchday = new SelectList(db.Matchdays, "matchday1", "matchday1", match.matchday);
            ViewBag.away = new SelectList(db.Teams, "iD", "abbreviation", match.away);
            ViewBag.home = new SelectList(db.Teams, "iD", "abbreviation", match.home);
            return View(match);
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(decimal id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.matchday = new SelectList(db.Matchdays, "matchday1", "matchday1", match.matchday);
            ViewBag.away = new SelectList(db.Teams, "iD", "abbreviation", match.away);
            ViewBag.home = new SelectList(db.Teams, "iD", "abbreviation", match.home);
            ViewBag.startYear = new SelectList(db.Seasons, "startYear", "startYear");
            ViewBag.competition = new SelectList(db.Competitions, "iD", "complete_Name");
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,home,away,goalsHome,goalsAway,matchday,startYear,competition")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return BackToMatches(match.competition, match.startYear);
            }
            ViewBag.matchday = new SelectList(db.Matchdays, "matchday1", "matchday1", match.matchday);
            ViewBag.away = new SelectList(db.Teams, "iD", "abbreviation", match.away);
            ViewBag.home = new SelectList(db.Teams, "iD", "abbreviation", match.home);
            ViewBag.startYear = new SelectList(db.Seasons, "startYear", "startYear");
            return View(match);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(decimal id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }


        public ActionResult BackToMatches(decimal c, decimal y)
        {
            return RedirectToAction("Matchdays", "Competitions", new { c = c, y = y });
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
            db.SaveChanges();
            return BackToMatches(match.competition, match.startYear);
        }

        public ActionResult GotToLineUp(decimal match, decimal team)
        {
            string query = "SELECT * FROM LineUp l inner join Player_Team p on (l.player = p.player) WHERE l.match = @p0 and p.team = @p1 ";
            IEnumerable<FootballManagerApp.Models.LineUp> lineUp = db.LineUps.SqlQuery(query, match, team).ToList();
            if (lineUp.Count() == 0)
                return RedirectToAction("CreateFixed", "LineUps", new { match = match, team = team });
            return RedirectToAction("LineUp", "Competitions", new { match = match, team = team });
        }

        public ActionResult RegisterGoals(decimal match, decimal team)
        {
            return RedirectToAction("CreateFixed","Goals", new { match = match, team = team });
        }

        public ActionResult WatchGoals(decimal match, decimal team)
        {
            return RedirectToAction("IndexMatch", "Goals", new { match = match, team = team });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
