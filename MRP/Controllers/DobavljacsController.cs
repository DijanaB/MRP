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
    public class DobavljacsController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Dobavljacs
        public ActionResult Index()
        {
            var dobavljacs = db.Dobavljacs.Include(d => d.Drzava1);
            return View(dobavljacs.ToList());
        }

        // GET: Dobavljacs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dobavljac dobavljac = db.Dobavljacs.Find(id);
            if (dobavljac == null)
            {
                return HttpNotFound();
            }
            return View(dobavljac);
        }

        // GET: Dobavljacs/Create
        public ActionResult Create()
        {
            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv");
            return View();
        }

        // POST: Dobavljacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon")] Dobavljac dobavljac)
        {
            if (ModelState.IsValid)
            {
                db.Dobavljacs.Add(dobavljac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", dobavljac.Drzava);
            return View(dobavljac);
        }

        // GET: Dobavljacs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dobavljac dobavljac = db.Dobavljacs.Find(id);
            if (dobavljac == null)
            {
                return HttpNotFound();
            }
            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", dobavljac.Drzava);
            return View(dobavljac);
        }

        // POST: Dobavljacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon")] Dobavljac dobavljac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dobavljac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", dobavljac.Drzava);
            return View(dobavljac);
        }

        // GET: Dobavljacs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dobavljac dobavljac = db.Dobavljacs.Find(id);
            if (dobavljac == null)
            {
                return HttpNotFound();
            }
            return View(dobavljac);
        }

        // POST: Dobavljacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Dobavljac dobavljac = db.Dobavljacs.Find(id);
            db.Dobavljacs.Remove(dobavljac);
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
