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
    public class PlayerTeamController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: PlayerTeam
        public ActionResult Index()
        {
            var player_Team = db.Player_Team.Include(p => p.Player1).Include(p => p.Team1);
            return View(player_Team.ToList());
        }

        // GET: PlayerTeam/Details/5
        public ActionResult Details(decimal player, decimal team, DateTime signDate)
        {
            Player_Team player_Team = db.Player_Team.Find(player, team, signDate);
            if (player_Team == null)
            {
                return HttpNotFound();
            }
            return View(player_Team);
        }

        // GET: PlayerTeam/Create
        public ActionResult Create()
        {
            ViewBag.player = new SelectList(db.Players, "iD", "firstName");
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation");
            return View();
        }

        // POST: PlayerTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "player,team,wage,signDate,expirationDate,breakUpDate")] Player_Team player_Team)
        {
            if (ModelState.IsValid)
            {
                db.Player_Team.Add(player_Team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.player = new SelectList(db.Players, "iD", "firstName", player_Team.player);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", player_Team.team);
            return View(player_Team);
        }

        // GET: PlayerTeam/Edit/5
        public ActionResult Edit(decimal player, decimal team, DateTime signDate)
        {
            Player_Team player_Team = db.Player_Team.Find(player, team, signDate);
            if (player_Team == null)
            {
                return HttpNotFound();
            }
            ViewBag.player = new SelectList(db.Players, "iD", "firstName", player_Team.player);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", player_Team.team);
            return View(player_Team);
        }

        // POST: PlayerTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "player,team,wage,signDate,expirationDate,breakUpDate")] Player_Team player_Team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player_Team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.player = new SelectList(db.Players, "iD", "firstName", player_Team.player);
            ViewBag.team = new SelectList(db.Teams, "iD", "abbreviation", player_Team.team);
            return View(player_Team);
        }

        // GET: PlayerTeam/Delete/5
        public ActionResult Delete(decimal player, decimal team, DateTime signDate)
        {
            Player_Team player_Team = db.Player_Team.Find(player, team, signDate);
            if (player_Team == null)
            {
                return HttpNotFound();
            }
            return View(player_Team);
        }

        // POST: PlayerTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Player_Team player_Team = db.Player_Team.Find(id);
            db.Player_Team.Remove(player_Team);
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
