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
    public class ZaposlenisController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Zaposlenis
        public ActionResult Index()
        {
            var zaposlenis = db.Zaposlenis.Include(z => z.Drzava1).Include(z => z.RadnoMesto1);
            return View(zaposlenis.ToList());
        }

        // GET: Zaposlenis/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = db.Zaposlenis.Find(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // GET: Zaposlenis/Create
        public ActionResult Create()
        {
            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv");
            ViewBag.RadnoMesto = new SelectList(db.RadnoMestoes, "Id", "Naziv");
            return View();
        }

        // POST: Zaposlenis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Prezime,Plata,DatumRodjenja,Adresa,Grad,Drzava,RadnoMesto,Kontakt")] Zaposleni zaposleni)
        {
            if (ModelState.IsValid)
            {
                db.Zaposlenis.Add(zaposleni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", zaposleni.Drzava);
            ViewBag.RadnoMesto = new SelectList(db.RadnoMestoes, "Id", "Naziv", zaposleni.RadnoMesto);
            return View(zaposleni);
        }

        // GET: Zaposlenis/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = db.Zaposlenis.Find(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", zaposleni.Drzava);
            ViewBag.RadnoMesto = new SelectList(db.RadnoMestoes, "Id", "Naziv", zaposleni.RadnoMesto);
            return View(zaposleni);
        }

        // POST: Zaposlenis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime,Prezime,Plata,DatumRodjenja,Adresa,Grad,Drzava,RadnoMesto,Kontakt")] Zaposleni zaposleni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zaposleni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", zaposleni.Drzava);
            ViewBag.RadnoMesto = new SelectList(db.RadnoMestoes, "Id", "Naziv", zaposleni.RadnoMesto);
            return View(zaposleni);
        }

        // GET: Zaposlenis/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = db.Zaposlenis.Find(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // POST: Zaposlenis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Zaposleni zaposleni = db.Zaposlenis.Find(id);
            db.Zaposlenis.Remove(zaposleni);
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
