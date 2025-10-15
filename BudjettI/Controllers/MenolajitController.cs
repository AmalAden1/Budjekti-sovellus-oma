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
    public class MenolajitController : Controller
    {

        private BudjektiBDEntities db = new BudjektiBDEntities();

        // GET: Menolajit
        public ActionResult Index()
        {
            return View(db.Menolajit.ToList());
        }

        // GET: Menolajit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menolajit menolajit = db.Menolajit.Find(id);
            if (menolajit == null)
            {
                return HttpNotFound();
            }
            return View(menolajit);
        }

        // GET: Menolajit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menolajit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenolajitID,Nimi")] Menolajit menolajit)
        {
            if (ModelState.IsValid)
            {
                db.Menolajit.Add(menolajit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menolajit);
        }

        // GET: Menolajit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menolajit menolajit = db.Menolajit.Find(id);
            if (menolajit == null)
            {
                return HttpNotFound();
            }
            return View(menolajit);
        }

        // POST: Menolajit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenolajitID,Nimi")] Menolajit menolajit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menolajit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menolajit);
        }

        // GET: Menolajit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menolajit menolajit = db.Menolajit.Find(id);
            if (menolajit == null)
            {
                return HttpNotFound();
            }
            return View(menolajit);
        }

        // POST: Menolajit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menolajit menolajit = db.Menolajit.Find(id);
            db.Menolajit.Remove(menolajit);
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
