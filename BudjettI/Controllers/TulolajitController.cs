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
    public class TulolajitController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        //Searchin takakoodi
        public ActionResult Index(string searchString1)
        {

            var tulolajit = from t in db.Tulolajit
                        select t;
            if (!String.IsNullOrEmpty(searchString1))
            {
                tulolajit = tulolajit.Where(t => t.Nimi.Contains(searchString1));
            }
            return View(tulolajit);
        }

        // GET: Tulolajit
        //public ActionResult Index()
        //{
        //    return View(db.Tulolajit.ToList());
        //}

        // GET: Tulolajit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tulolajit tulolajit = db.Tulolajit.Find(id);
            if (tulolajit == null)
            {
                return HttpNotFound();
            }
            return View(tulolajit);
        }

        // GET: Tulolajit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tulolajit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TulolajitID,Nimi")] Tulolajit tulolajit)
        {
            if (ModelState.IsValid)
            {
                db.Tulolajit.Add(tulolajit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tulolajit);
        }

        // GET: Tulolajit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tulolajit tulolajit = db.Tulolajit.Find(id);
            if (tulolajit == null)
            {
                return HttpNotFound();
            }
            return View(tulolajit);
        }

        // POST: Tulolajit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TulolajitID,Nimi")] Tulolajit tulolajit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tulolajit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tulolajit);
        }

        // GET: Tulolajit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tulolajit tulolajit = db.Tulolajit.Find(id);
            if (tulolajit == null)
            {
                return HttpNotFound();
            }
            return View(tulolajit);
        }

        // POST: Tulolajit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tulolajit tulolajit = db.Tulolajit.Find(id);
            db.Tulolajit.Remove(tulolajit);
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
