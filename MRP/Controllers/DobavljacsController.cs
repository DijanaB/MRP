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
            if (User.Identity.IsAuthenticated)
            {
                var dobavljacs = db.Dobavljacs.Include(d => d.Drzava1);
                return View(dobavljacs.OrderBy(x => x.Naziv).ToList());

            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: Dobavljacs/Details/5
        public ActionResult Details(long? id)
        {
            if (User.Identity.IsAuthenticated)
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
            else
            {
                return HttpNotFound();
            }

            
        }

        // GET: Dobavljacs/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv");
                return View();

            }
            else
            {
                return HttpNotFound();
            }
           
        }

        // POST: Dobavljacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon")] Dobavljac dobavljac)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (db.Dobavljacs.Any(x => x.Naziv.Equals(dobavljac.Naziv)))
                    {
                        ModelState.AddModelError("Naziv", "Dobavljac sa unetim nazivom već postoji.");
                    }
                    if (db.Dobavljacs.Any(x => x.Email.Equals(dobavljac.Email)))
                    {
                        ModelState.AddModelError("Email", "Dobavljac sa unetim email-om već postoji.");
                    }

                    if (db.Dobavljacs.Any(x => x.KontaktTelefon.Equals(dobavljac.KontaktTelefon)))
                    {
                        ModelState.AddModelError("Email", "Dobavljac sa unetim kontakt telefonom već postoji.");
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(dobavljac);
                    }
                    db.Dobavljacs.Add(dobavljac);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", dobavljac.Drzava);
                return View(dobavljac);

            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: Dobavljacs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (User.Identity.IsAuthenticated)
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
            else
            {
                return HttpNotFound();
            }
           
        }

        // POST: Dobavljacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Adresa,Grad,Drzava,Email,KontaktTelefon")] Dobavljac dobavljac)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (db.Dobavljacs.Any(x => x.Id != dobavljac.Id && x.Naziv.Equals(dobavljac.Naziv)))
                    {
                        ModelState.AddModelError("Naziv", "Dobavljac sa unetim nazivom već postoji.");
                    }
                    if (db.Dobavljacs.Any(x => x.Id != dobavljac.Id && x.Email.Equals(dobavljac.Email)))
                    {
                        ModelState.AddModelError("Email", "Dobavljac sa unetim email-om već postoji.");
                    }

                    if (db.Dobavljacs.Any(x => x.Id != dobavljac.Id && x.KontaktTelefon.Equals(dobavljac.KontaktTelefon)))
                    {
                        ModelState.AddModelError("Email", "Dobavljac sa unetim kontakt telefonom već postoji.");
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(dobavljac);
                    }

                    db.Entry(dobavljac).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Drzava = new SelectList(db.Drzavas, "Id", "Naziv", dobavljac.Drzava);
                return View(dobavljac);

            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: Dobavljacs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (User.Identity.IsAuthenticated)
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
            else
            {
                return HttpNotFound();
            }
            
        }

        // POST: Dobavljacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Dobavljac dobavljac = db.Dobavljacs.Find(id);
                if (db.Opremas.Any(x => x.Dobavljac == id) || db.Materijals.Any(x => x.Dobavljac == id))
                {
                    ModelState.AddModelError("Naziv", "Dobavljac je u upotrebi.");
                    return View(dobavljac);
                }
                db.Dobavljacs.Remove(dobavljac);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return HttpNotFound();
            }
            
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
