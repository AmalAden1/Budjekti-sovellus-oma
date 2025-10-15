using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using BudjettI.Models;

namespace BudjettI.Controllers
{
    public class TulotController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        //Searchin takakoodi, TÄMÄ EI TOI, KOSKA HERJAA TulolajitID, on INT eikä String. Kysytään opettajalta
        //public ActionResult Index(string searchString1)
        //{

        //    var tulot = from t in db.Tulot
        //                    select t;
        //    if (!String.IsNullOrEmpty(searcString1))
        //    {
        //        tulot = tulot.Where(t => t.TulolajitID.Contains(searchString1));
        //    }
        //    return View(tulot);
        //}



        //// GET: Tulot
        public ActionResult Index()
        {
            var tulot = db.Tulot.Include(t => t.Asiakas).Include(t => t.Tulolajit);
            return View(tulot.ToList());
        }

        // GET: Tulot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tulot tulot = db.Tulot.Find(id);
            if (tulot == null)
            {
                return HttpNotFound();
            }
            return View(tulot);
        }

        // GET: Tulot/Create
        public ActionResult Create()
        {
            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi");
            ViewBag.TulolajitID = new SelectList(db.Tulolajit, "TulolajitID", "Nimi");
            return View();
        }

        // POST: Tulot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TulotID,AsiakasID,TulolajitID,PVM,Maara")] Tulot tulot)
        {
            if (ModelState.IsValid)
            {
                db.Tulot.Add(tulot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi", tulot.AsiakasID);
            ViewBag.TulolajitID = new SelectList(db.Tulolajit, "TulolajitID", "Nimi", tulot.TulolajitID);
            return View(tulot);
        }

        // GET: Tulot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tulot tulot = db.Tulot.Find(id);
            if (tulot == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi", tulot.AsiakasID);
            ViewBag.TulolajitID = new SelectList(db.Tulolajit, "TulolajitID", "Nimi", tulot.TulolajitID);
            return View(tulot);
        }

        // POST: Tulot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TulotID,AsiakasID,TulolajitID,PVM,Maara")] Tulot tulot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tulot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakas, "AsiakasID", "Etunimi", tulot.AsiakasID);
            ViewBag.TulolajitID = new SelectList(db.Tulolajit, "TulolajitID", "Nimi", tulot.TulolajitID);
            return View(tulot);
        }

        // GET: Tulot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tulot tulot = db.Tulot.Find(id);
            if (tulot == null)
            {
                return HttpNotFound();
            }
            return View(tulot);
        }

        // POST: Tulot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tulot tulot = db.Tulot.Find(id);
            db.Tulot.Remove(tulot);
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
