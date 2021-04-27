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
    public class SastojaksController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Sastojaks
        public ActionResult Index()
        {
            var sastojaks = db.Sastojaks.Include(s => s.Materijal1).Include(s => s.Proizvod1);
            return View(sastojaks.ToList());
        }

        // GET: Sastojaks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sastojak sastojak = db.Sastojaks.Find(id);
            if (sastojak == null)
            {
                return HttpNotFound();
            }
            return View(sastojak);
        }

        // GET: Sastojaks/Create
        public ActionResult Create()
        {
            ViewBag.Materijal = new SelectList(db.Materijals, "Id", "Naziv");
            ViewBag.Proizvod = new SelectList(db.Proizvods, "Id", "Naziv");
            return View();
        }

        // POST: Sastojaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Kolicina,Materijal,Proizvod")] Sastojak sastojak)
        {
            if (ModelState.IsValid)
            {
                db.Sastojaks.Add(sastojak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Materijal = new SelectList(db.Materijals, "Id", "Naziv", sastojak.Materijal);
            ViewBag.Proizvod = new SelectList(db.Proizvods, "Id", "Naziv", sastojak.Proizvod);
            return View(sastojak);
        }

        // GET: Sastojaks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sastojak sastojak = db.Sastojaks.Find(id);
            if (sastojak == null)
            {
                return HttpNotFound();
            }
            ViewBag.Materijal = new SelectList(db.Materijals, "Id", "Naziv", sastojak.Materijal);
            ViewBag.Proizvod = new SelectList(db.Proizvods, "Id", "Naziv", sastojak.Proizvod);
            return View(sastojak);
        }

        // POST: Sastojaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Kolicina,Materijal,Proizvod")] Sastojak sastojak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sastojak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Materijal = new SelectList(db.Materijals, "Id", "Naziv", sastojak.Materijal);
            ViewBag.Proizvod = new SelectList(db.Proizvods, "Id", "Naziv", sastojak.Proizvod);
            return View(sastojak);
        }

        // GET: Sastojaks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sastojak sastojak = db.Sastojaks.Find(id);
            if (sastojak == null)
            {
                return HttpNotFound();
            }
            return View(sastojak);
        }

        // POST: Sastojaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sastojak sastojak = db.Sastojaks.Find(id);
            db.Sastojaks.Remove(sastojak);
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
