using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballManagerApp.Models;

namespace FootballManagerApp.Controllers
{
    public class CoachesController : Controller
    {
        private FootballManagerDBEntities db = new FootballManagerDBEntities();

        // GET: Coaches
        public ActionResult Index()
        {
            return View(db.Coaches.ToList());
        }

        // GET: Coaches/Details/5
        public ActionResult Details(decimal id)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(Procedures.Coaches((int)id));
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            return View();
        }

        /**
         * POST: Coaches/Create
         * To protect from overposting attacks, please enable the specific properties you want to bind to, for
         * more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost] [ValidateAntiForgeryToken] public ActionResult Create([Bind(Include = "iD,coach_Name,imageURL,birthdate,wasPlayer,dateStart")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                coach.imageURL = new byte[0];
                db.Coaches.Add(coach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coach);
        }

        // GET: Coaches/Edit/5
        public ActionResult Edit(decimal id)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        /**
         * POST: Coaches/Edit/5
         * To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         * more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost] [ValidateAntiForgeryToken] public ActionResult Edit([Bind(Include = "iD,coach_Name,imageURL,birthdate,wasPlayer,dateStart")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                coach.imageURL = new byte[0];
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public ActionResult Delete(decimal id)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Coach coach = db.Coaches.Find(id);
            db.Coaches.Remove(coach);
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

        /**
         * IMAGE PARSING
         * Image -> byte[]
         * In: Image to parse
         * Out: Byte array that represents the image.
         */
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }


    }
}
