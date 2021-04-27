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
    public class MaterijalsController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Materijals
        public ActionResult Index()
        {
            var materijals = db.Materijals.Include(m => m.Dobavljac1).Include(m => m.Skladiste1);
            return View(materijals.ToList());
        }

        // GET: Materijals/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materijal materijal = db.Materijals.Find(id);
            if (materijal == null)
            {
                return HttpNotFound();
            }
            return View(materijal);
        }

        // GET: Materijals/Create
        public ActionResult Create()
        {
            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv");
            ViewBag.Skladiste = new SelectList(db.Skladistes, "Id", "Naziv");
            return View();
        }

        // POST: Materijals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,Skladiste,Dobavljac,RokTrajanja")] Materijal materijal)
        {
            if (ModelState.IsValid)
            {
                db.Materijals.Add(materijal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv", materijal.Dobavljac);
            ViewBag.Skladiste = new SelectList(db.Skladistes, "Id", "Naziv", materijal.Skladiste);
            return View(materijal);
        }

        // GET: Materijals/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materijal materijal = db.Materijals.Find(id);
            if (materijal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv", materijal.Dobavljac);
            ViewBag.Skladiste = new SelectList(db.Skladistes, "Id", "Naziv", materijal.Skladiste);
            return View(materijal);
        }

        // POST: Materijals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Kolicina,CenaPoJediniciMere,JedinicaMere,UkupnaCena,Skladiste,Dobavljac,RokTrajanja")] Materijal materijal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materijal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv", materijal.Dobavljac);
            ViewBag.Skladiste = new SelectList(db.Skladistes, "Id", "Naziv", materijal.Skladiste);
            return View(materijal);
        }

        // GET: Materijals/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materijal materijal = db.Materijals.Find(id);
            if (materijal == null)
            {
                return HttpNotFound();
            }
            return View(materijal);
        }

        // POST: Materijals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Materijal materijal = db.Materijals.Find(id);
            db.Materijals.Remove(materijal);
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
