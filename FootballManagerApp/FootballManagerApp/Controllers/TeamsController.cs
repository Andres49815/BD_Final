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
    public class TeamsController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: Teams
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.Association1);
            return View(teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(decimal id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName");
            return View();
        }

        /**
         * POST: Teams/Create
         * To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         * more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost] [ValidateAntiForgeryToken] public ActionResult Create([Bind(Include = "iD,abbreviation,completeName,formation,association")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName", team.association);
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(decimal id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName", team.association);
            return View(team);
        }

        /**
         * POST: Teams/Edit/5
         * To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         * more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost] [ValidateAntiForgeryToken] public ActionResult Edit([Bind(Include = "iD,abbreviation,completeName,formation,association")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.association = new SelectList(db.Associations, "abbreviation", "completeName", team.association);
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(decimal id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken] public ActionResult DeleteConfirmed(decimal id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
