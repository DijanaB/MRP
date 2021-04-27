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
    public class FazaProizvodnjesController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: FazaProizvodnjes
        public ActionResult Index()
        {
            return View(db.FazaProizvodnjes.OrderBy(x=>x.Naziv).ToList());
        }

        // GET: FazaProizvodnjes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FazaProizvodnje fazaProizvodnje = db.FazaProizvodnjes.Find(id);
            if (fazaProizvodnje == null)
            {
                return HttpNotFound();
            }
            return View(fazaProizvodnje);
        }

        // GET: FazaProizvodnjes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FazaProizvodnjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] FazaProizvodnje fazaProizvodnje)
        {
            if (ModelState.IsValid)
            {
                if (db.FazaProizvodnjes.Any(x => x.Naziv.Equals(fazaProizvodnje.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Faza proizvodnje sa unetim nazivom već postoji.");
                    return View(fazaProizvodnje);
                }

                db.FazaProizvodnjes.Add(fazaProizvodnje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fazaProizvodnje);
        }

        // GET: FazaProizvodnjes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FazaProizvodnje fazaProizvodnje = db.FazaProizvodnjes.Find(id);
            if (fazaProizvodnje == null)
            {
                return HttpNotFound();
            }
            return View(fazaProizvodnje);
        }

        // POST: FazaProizvodnjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] FazaProizvodnje fazaProizvodnje)
        {
            if (ModelState.IsValid)
            {
                if (db.FazaProizvodnjes.Any(x => x.Id != fazaProizvodnje.Id && x.Naziv.Equals(fazaProizvodnje.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Faza proizvodnje sa unetim nazivom već postoji.");
                    return View(fazaProizvodnje);
                }
                db.Entry(fazaProizvodnje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fazaProizvodnje);
        }

        // GET: FazaProizvodnjes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FazaProizvodnje fazaProizvodnje = db.FazaProizvodnjes.Find(id);
            if (fazaProizvodnje == null)
            {
                return HttpNotFound();
            }
            return View(fazaProizvodnje);
        }

        // POST: FazaProizvodnjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FazaProizvodnje fazaProizvodnje = db.FazaProizvodnjes.Find(id);
            if (db.VremenaProizvodnjes.Any(x => x.FazaProizvodnje.HasValue && x.FazaProizvodnje.Value == id))
            {
                ModelState.AddModelError("Naziv", "Faza proizvodnje je u upotrebi.");
                return View(fazaProizvodnje);
            }
            db.FazaProizvodnjes.Remove(fazaProizvodnje);
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
