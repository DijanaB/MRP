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
    public class OpremasController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Opremas
        public ActionResult Index()
        {
            var opremas = db.Opremas.Include(o => o.Dobavljac1);
            return View(opremas.ToList());
        }

        // GET: Opremas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oprema oprema = db.Opremas.Find(id);
            if (oprema == null)
            {
                return HttpNotFound();
            }
            return View(oprema);
        }

        // GET: Opremas/Create
        public ActionResult Create()
        {
            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv");
            return View();
        }

        // POST: Opremas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,DatumKupovine,GodisnjaAmortizacija,Dobavljac,PocetnaCena,TrenutnaVrednost")] Oprema oprema)
        {
            if (ModelState.IsValid)
            {
                db.Opremas.Add(oprema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv", oprema.Dobavljac);
            return View(oprema);
        }

        // GET: Opremas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oprema oprema = db.Opremas.Find(id);
            if (oprema == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv", oprema.Dobavljac);
            return View(oprema);
        }

        // POST: Opremas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,DatumKupovine,GodisnjaAmortizacija,Dobavljac,PocetnaCena,TrenutnaVrednost")] Oprema oprema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oprema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dobavljac = new SelectList(db.Dobavljacs, "Id", "Naziv", oprema.Dobavljac);
            return View(oprema);
        }

        // GET: Opremas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oprema oprema = db.Opremas.Find(id);
            if (oprema == null)
            {
                return HttpNotFound();
            }
            return View(oprema);
        }

        // POST: Opremas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Oprema oprema = db.Opremas.Find(id);
            db.Opremas.Remove(oprema);
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
