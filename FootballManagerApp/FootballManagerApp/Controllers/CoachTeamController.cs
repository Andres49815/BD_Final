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
    public class CoachTeamController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: CoachTeam
        public ActionResult Index()
        {
            var coach_Team = db.Coach_Team.Include(c => c.Coach1).Include(c => c.Team1);
            return View(coach_Team.ToList());
        }

        // GET: CoachTeam/Details/5
        public ActionResult Details(decimal coach, decimal team, DateTime signDate)
        {
            Coach_Team coach_Team = db.Coach_Team.Find(coach, team, signDate);
            if (coach_Team == null)
            {
                return HttpNotFound();
            }
            return View(coach_Team);
        }

        // GET: CoachTeam/Create
        public ActionResult Create()
        {
            ViewBag.coach = new SelectList(db.Coaches, "iD", "coach_Name");
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation");
            return View();
        }

        // POST: CoachTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "team,coach,signDate,expirationDate,breakupDate,wage")] Coach_Team coach_Team)
        {
            if (ModelState.IsValid)
            {
                db.Coach_Team.Add(coach_Team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.coach = new SelectList(db.Coaches, "iD", "coach_Name", coach_Team.coach);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", coach_Team.team);
            return View(coach_Team);
        }

        // GET: CoachTeam/Edit/5
        public ActionResult Edit(decimal coach, decimal team, DateTime signDate)
        {
            Coach_Team coach_Team = db.Coach_Team.Find(coach, team, signDate);
            if (coach_Team == null)
            {
                return HttpNotFound();
            }
            ViewBag.coach = new SelectList(db.Coaches, "iD", "coach_Name", coach_Team.coach);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", coach_Team.team);
            return View(coach_Team);
        }

        // POST: CoachTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "team,coach,signDate,expirationDate,breakupDate,wage")] Coach_Team coach_Team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach_Team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.coach = new SelectList(db.Coaches, "iD", "coach_Name", coach_Team.coach);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", coach_Team.team);
            return View(coach_Team);
        }

        // GET: CoachTeam/Delete/5
        public ActionResult Delete(decimal coach, decimal team, DateTime signDate)
        {
            Coach_Team coach_Team = db.Coach_Team.Find(coach, team, signDate);
            if (coach_Team == null)
            {
                return HttpNotFound();
            }
            return View(coach_Team);
        }

        // POST: CoachTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Coach_Team coach_Team = db.Coach_Team.Find(id);
            db.Coach_Team.Remove(coach_Team);
            db.SaveChanges();
            return RedirectToAction("Index");
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
