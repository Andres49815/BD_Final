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
    public class RefereesController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();
        private string connectionInfo = "data source=ecRhin.ec.tec.ac.cr\\Estudiantes;initial catalog=FootballManagerDB;persist security info=True;user=anobando;" +
            "password=anobando;MultipleActiveResultSets=True;App=EntityFramework";

        // GET: Referees
        public ActionResult Index()
        {
            //SqlConnection connection = new SqlConnection(connectionInfo);
            //connection.Open();
            //SqlCommand cmd = new SqlCommand("Insertar_Prueba", connection);
            //SqlParameter parameter = new SqlParameter("@id", "000001");
            //cmd.Parameters.Add(parameter);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.ExecuteNonQuery();
            //connection.Close();
            // Borrar despues
            var referees = db.Referees.Include(r => r.Country1);
            return View(referees.ToList());
        }

        // GET: Referees/Details/5
        public ActionResult Details(decimal id)
        {
            Referee referee = db.Referees.Find(id);
            if (referee == null)
            {
                return HttpNotFound();
            }
            return View(referee);
        }

        // GET: Referees/Create
        public ActionResult Create()
        {
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName");
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName");
            return View();
        }

        // POST: Referees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,country")] Referee referee)
        {
            if (ModelState.IsValid)
            {
                db.Referees.Add(referee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName", referee.country);
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName", referee.country);
            return View(referee);
        }

        // GET: Referees/Edit/5
        public ActionResult Edit(decimal id)
        {
            Referee referee = db.Referees.Find(id);
            if (referee == null)
            {
                return HttpNotFound();
            }
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName", referee.country);
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName", referee.country);
            return View(referee);
        }

        // POST: Referees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,country")] Referee referee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName", referee.country);
            ViewBag.country = new SelectList(db.Countries, "abbreviation", "completeName", referee.country);
            return View(referee);
        }

        // GET: Referees/Delete/5
        public ActionResult Delete(decimal id)
        {
            Referee referee = db.Referees.Find(id);
            if (referee == null)
            {
                return HttpNotFound();
            }
            return View(referee);
        }

        public ActionResult WatchGoals(decimal match, decimal team)
        {
            return RedirectToAction("IndexMatch", "Match" ,new { match = match, team = team});
        }

        // POST: Referees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Referee referee = db.Referees.Find(id);
            db.Referees.Remove(referee);
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
