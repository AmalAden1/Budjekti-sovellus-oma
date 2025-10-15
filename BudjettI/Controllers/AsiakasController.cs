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
    public class AsiakasController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        // GET: Asiakas
        public ActionResult Index()
        {
            return View(db.Asiakas.ToList());
        }

        // GET: Asiakas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakas asiakas = db.Asiakas.Find(id);
            if (asiakas == null)
            {
                return HttpNotFound();
            }
            return View(asiakas);
        }

        // GET: Asiakas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asiakas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Sahkoposti")] Asiakas asiakas)
        {
            if (ModelState.IsValid)
            {
                db.Asiakas.Add(asiakas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asiakas);
        }

        // GET: Asiakas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakas asiakas = db.Asiakas.Find(id);
            if (asiakas == null)
            {
                return HttpNotFound();
            }
            return View(asiakas);
        }

        // POST: Asiakas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Sahkoposti")] Asiakas asiakas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakas);
        }

        // GET: Asiakas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakas asiakas = db.Asiakas.Find(id);
            if (asiakas == null)
            {
                return HttpNotFound();
            }
            return View(asiakas);
        }

        // POST: Asiakas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakas asiakas = db.Asiakas.Find(id);
            db.Asiakas.Remove(asiakas);
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
