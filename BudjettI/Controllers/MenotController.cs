using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudjettI.Models;

namespace BudjettI.Controllers
{
    public class MenotController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        // GET: Menot
        public ActionResult Index()
        {
            var menot = db.Menot.Include(m => m.Asiakas).Include(m => m.Menolajit);
            return View(menot.ToList());
        }

        // GET: Menot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menot menot = db.Menot.Find(id);
            if (menot == null)
            {
                return HttpNotFound();
            }
            return View(menot);
        }

        // GET: Menot/Create
        public ActionResult Create()
        {
            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi");
            ViewBag.MenolajitID = new SelectList(db.Menolajit, "MenolajitID", "Nimi");
            return View();
        }

        // POST: Menot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenotID,AsiakasID,MenolajitID,PVM,Maara")] Menot menot)
        {
            if (ModelState.IsValid)
            {
                db.Menot.Add(menot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi", menot.AsiakasID);
            ViewBag.MenolajitID = new SelectList(db.Menolajit, "MenolajitID", "Nimi", menot.MenolajitID);
            return View(menot);
        }

        // GET: Menot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menot menot = db.Menot.Find(id);
            if (menot == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi", menot.AsiakasID);
            ViewBag.MenolajitID = new SelectList(db.Menolajit, "MenolajitID", "Nimi", menot.MenolajitID);
            return View(menot);
        }

        // POST: Menot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenotID,AsiakasID,MenolajitID,PVM,Maara")] Menot menot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi", menot.AsiakasID);
            ViewBag.MenolajitID = new SelectList(db.Menolajit, "MenolajitID", "Nimi", menot.MenolajitID);
            return View(menot);
        }

        // GET: Menot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menot menot = db.Menot.Find(id);
            if (menot == null)
            {
                return HttpNotFound();
            }
            return View(menot);
        }

        // POST: Menot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menot menot = db.Menot.Find(id);
            db.Menot.Remove(menot);
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
