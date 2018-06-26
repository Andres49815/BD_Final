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
    public class GoalsController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: Goals
        public ActionResult Index()
        {
            var goals = db.Goals.Include(g => g.LineUp).Include(g => g.goalType);
            return View(goals.ToList());
        }

        [ActionName("IndexMatch")]
        public ActionResult Index(decimal match, decimal team)
        { 
            string query = "SELECT distinct * FROM Goals g INNER JOIN LineUp l on (l.match = g.match and l.team = g.team and g.player = l.player) INNER JOIN goalType gt on (g.gType = gt.goalType) WHERE g.match = @p0 and g.team = @p1";
            var goals = db.Goals.SqlQuery(query, match, team);
            return View(goals.ToList());
        }

        // GET: Goals/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // GET: Goals/Create
        public ActionResult Create()
        {
            ViewBag.player = new SelectList(db.LineUps, "player", "position");
            ViewBag.gType = new SelectList(db.goalTypes, "goalType1", "goalType1");
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "match,player,minute,team,gType")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(goal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.player = new SelectList(db.LineUps, "player", "position", goal.player);
            ViewBag.gType = new SelectList(db.goalTypes, "goalType1", "goalType1", goal.gType);
            return View(goal);
        }

        // GET: Goals/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            ViewBag.player = new SelectList(db.LineUps, "player", "position", goal.player);
            ViewBag.gType = new SelectList(db.goalTypes, "goalType1", "goalType1", goal.gType);
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "match,player,minute,team,gType")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.player = new SelectList(db.LineUps, "player", "position", goal.player);
            ViewBag.gType = new SelectList(db.goalTypes, "goalType1", "goalType1", goal.gType);
            return View(goal);
        }

        // GET: Goals/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Goal goal = db.Goals.Find(id);
            db.Goals.Remove(goal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [ActionName("CreateFixed")]
        public ActionResult Create(decimal match, decimal team)
        {
            string query = "SELECT * FROM LineUp WHERE match = @p0 and team = @p1";
            ViewBag.player = new SelectList(db.LineUps.SqlQuery(query, match, team), "player", "position");
            ViewBag.gType = new SelectList(db.goalTypes, "goalType1", "goalType1");
            query = "SELECT t.* FROM Team t WHERE t.iD = @p0";
            ViewBag.team = new SelectList(db.Teams.SqlQuery(query, team), "iD", "abbreviation");
            List<Match> matches = new List<Match>();
            foreach (var m in db.Matches.ToList())
                if (m.id == match)
                    matches.Add(m);
            ViewBag.match = new SelectList(matches, "id", "id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFixed([Bind(Include = "match,player,minute,team,gType")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(goal);
                db.SaveChanges();
                return RedirectToAction("IndexMatch", new { match = goal.match, team = goal.team});
            }
            string query = "SELECT * FROM LineUp WHERE match = @p0 and team = @p1";
            ViewBag.player = new SelectList(db.LineUps.SqlQuery(query, goal.match, goal.team), "player", "position");
            ViewBag.gType = new SelectList(db.goalTypes, "goalType1", "goalType1");
            query = "SELECT t.* FROM Team t WHERE t.iD = @p0";
            ViewBag.team = new SelectList(db.Teams.SqlQuery(query, goal.team), "iD", "abbreviation");
            List<Match> matches = new List<Match>();
            foreach (var m in db.Matches.ToList())
                if (m.id == goal.match)
                    matches.Add(m);
            ViewBag.match = new SelectList(matches, "id", "id");
            return View(goal);
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
