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
    public class LineUpsController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: LineUps
        public ActionResult Index()
        {
            var lineUps = db.LineUps.Include(l => l.Match1).Include(l => l.Player1).Include(l => l.PossiblePosition).Include(l => l.Team1);
            return View(lineUps.ToList());
        }

        // GET: LineUps/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.LineUps.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        // GET: LineUps/Create
        public ActionResult Create()
        {
            ViewBag.match = new SelectList(db.Matches, "id", "id");
            ViewBag.player = new SelectList(db.Players, "iD", "firstName");
            ViewBag.position = new SelectList(db.PossiblePositions, "abbreviation", "description");
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation");
            return View();
        }

        [ActionName("CreateFixed")]
        public ActionResult Create(decimal match, decimal team)
        {
            //ViewBag.match = new SelectList(db.Matches, "id", "id", match);
            string query = "SELECT p.* FROM Player p INNER JOIN Player_Team pt ON(p.iD = pt.player) WHERE pt.team = @p0 AND p.iD NOT IN (SELECT player FROM LineUp WHERE match = @p1)";
            ViewBag.player = new SelectList(db.Players.SqlQuery(query, team, match), "iD", "firstName");
            ViewBag.position = new SelectList(db.PossiblePositions, "abbreviation", "abbreviation");
            query = "SELECT t.* FROM Team t WHERE t.iD = @p0";
            ViewBag.team = new SelectList(db.Teams.SqlQuery(query, team), "iD", "abbreviation");
            List<Match> matches = new List<Match>();
            foreach (var m in db.Matches.ToList())
                if (m.id == match)
                    matches.Add(m);
            ViewBag.match = new SelectList(matches, "id", "id");
            return View();
        }

        // POST: LineUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "player,match,minuteIn,minuteOut,position,team")] LineUp lineUp)
        {
            if (ModelState.IsValid)
            {
                db.LineUps.Add(lineUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.match = new SelectList(db.Matches, "id", "id", lineUp.match);
            ViewBag.player = new SelectList(db.Players, "iD", "firstName", lineUp.player);
            ViewBag.position = new SelectList(db.PossiblePositions, "abbreviation", "abbreviation", lineUp.position);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", lineUp.team);
            return View(lineUp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFixed([Bind(Include = "player,match,minuteIn,minuteOut,position,team")] LineUp lineUp)
        {
            if (ModelState.IsValid)
            {
                db.LineUps.Add(lineUp);
                db.SaveChanges();
                return RedirectToAction("LineUp", "Competitions", new { match = lineUp.match, team = lineUp.team });
            }

            ViewBag.match = new SelectList(db.Matches, "id", "id", lineUp.match);
            string query = "SELECT p.* FROM Player p INNER JOIN Player_Team pt ON(p.iD = pt.player) WHERE pt.team = @p0 AND p.iD NOT IN (SELECT player FROM LineUp WHERE match = @p1)";
            ViewBag.player = new SelectList(db.Players.SqlQuery(query, lineUp.team, lineUp.match), "iD", "firstName");
            ViewBag.position = new SelectList(db.PossiblePositions, "abbreviation", "description");
            query = "SELECT t.* FROM Team t WHERE t.iD = @p0";
            ViewBag.team = new SelectList(db.Teams.SqlQuery(query, lineUp.team), "iD", "abbreviation");
            query = "SELECT * FROM Match m WHERE m.id = @p0";
            return View(lineUp);
        }

        // GET: LineUps/Edit/5
        public ActionResult Edit(decimal player, decimal match)
        {
            if (match == null || player == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.LineUps.Find(player, match);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            ViewBag.match = new SelectList(db.Matches, "id", "id", lineUp.match);
            ViewBag.player = new SelectList(db.Players, "iD", "firstName", lineUp.player);
            ViewBag.position = new SelectList(db.PossiblePositions, "abbreviation", "description", lineUp.position);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", lineUp.team);
            return View(lineUp);
        }

        // POST: LineUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "player,match,minuteIn,minuteOut,position,team")] LineUp lineUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LineUp", "Competitions", new { match = lineUp.match, team = lineUp.team });
            }
            ViewBag.match = new SelectList(db.Matches, "id", "id", lineUp.match);
            ViewBag.player = new SelectList(db.Players, "iD", "firstName", lineUp.player);
            ViewBag.position = new SelectList(db.PossiblePositions, "abbreviation", "description", lineUp.position);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", lineUp.team);
            return View(lineUp);
        }

        // GET: LineUps/Delete/5
        public ActionResult Delete(decimal player, decimal match)
        {
            if (match == null || player == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.LineUps.Find(player, match);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        public ActionResult GoToLineUp(decimal match, decimal team)
        {
            return RedirectToAction("LineUp", "Competitions", new { match = match, team = team});
        }

        // POST: LineUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal player, decimal match)
        {
            LineUp lineUp = db.LineUps.Find(player, match);
            db.LineUps.Remove(lineUp);
            db.SaveChanges();
            return RedirectToAction("LineUp", "Competitions", new { match = lineUp.match, team = lineUp.team });
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
