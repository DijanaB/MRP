using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MRP.Models;

namespace MRP.Controllers
{
    public class SkladistesController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Skladistes
        public ActionResult Index()
        {
            return View(db.Skladistes.ToList());
        }

        // GET: Skladistes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skladiste skladiste = db.Skladistes.Find(id);
            if (skladiste == null)
            {
                return HttpNotFound();
            }
            return View(skladiste);
        }

        // GET: Skladistes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skladistes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] Skladiste skladiste)
        {
            if (ModelState.IsValid)
            {
                db.Skladistes.Add(skladiste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skladiste);
        }

        // GET: Skladistes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skladiste skladiste = db.Skladistes.Find(id);
            if (skladiste == null)
            {
                return HttpNotFound();
            }
            return View(skladiste);
        }

        // POST: Skladistes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] Skladiste skladiste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skladiste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skladiste);
        }

        // GET: Skladistes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skladiste skladiste = db.Skladistes.Find(id);
            if (skladiste == null)
            {
                return HttpNotFound();
            }
            return View(skladiste);
        }

        // POST: Skladistes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Skladiste skladiste = db.Skladistes.Find(id);
            db.Skladistes.Remove(skladiste);
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
