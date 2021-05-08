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
    public class ProizvodsController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Proizvods
        public ActionResult Index()
        {
            List<Proizvod> proizvodi = db.Proizvods.OrderBy(x => x.Naziv).ToList();
            for(int i = 0; i< proizvodi.Count; i++)
            {
                proizvodi[i].Vreme = TimeSpan.FromTicks(proizvodi[i].VremePripreme).ToString(@"hh\:mm");
            }
            return View(proizvodi);
        }

        // GET: Proizvods/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            
            if (proizvod == null)
            {
                return HttpNotFound();
            }

            proizvod.Vreme = TimeSpan.FromTicks(proizvod.VremePripreme).ToString(@"hh\:mm");
            return View(proizvod);
        }

        // GET: Proizvods/Create
        public ActionResult Create()
        {
            Proizvod proizvod = new Proizvod();
            proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
            return View(proizvod);
        }

        // POST: Proizvods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,VremePripreme,Vreme")] Proizvod proizvod, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // Check name
                if (db.Proizvods.Any(x => x.Naziv.Equals(proizvod.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Proizvod sa unetim nazivom već postoji.");
                    proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                    return View(proizvod);
                }

                // Check time
                if (proizvod.Vreme == null || proizvod.Vreme.Length < 5)
                {
                    ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
                    proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                    return View(proizvod);
                }

                // => Split passed string
                string[] timeSplit = null;
                TimeSpan time = new TimeSpan();
                try
                {
                    // => Split passed string
                    timeSplit = proizvod.Vreme.Split(':');
                    time = new TimeSpan(int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
                    proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                    return View(proizvod);
                }

                // sastojci
                List<Materijal> materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                string[] sastojci = collection["Sastojci"].ToString().Split(',');
                string[] kolicina = collection["Kolicina"].ToString().Split(',');
                List<Sastojak> sastojciProizvoda = new List<Sastojak>();
                for(int i=0; i<sastojci.Length; i++)
                {
                    try
                    {
                        int id = int.Parse(sastojci[i]);
                        float kol = float.Parse(kolicina[i]);

                        Sastojak sastojak = new Sastojak
                        {
                            Materijal = id,
                            Kolicina = kol
                        };

                        if(sastojciProizvoda.Any(x=> x.Materijal == sastojak.Materijal))
                        {
                            ModelState.AddModelError("Materijali", "Odabrana su dva ista sastojka.");
                            proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                            return View(proizvod);
                        }
                        sastojciProizvoda.Add(sastojak);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("Materijali", "Sastojci nisu dobro unteti.");
                        proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                        return View(proizvod);
                    }
                }
                if (!sastojciProizvoda.Any())
                {
                    ModelState.AddModelError("Materijali", "Sastojci su obavezni.");
                    proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                    return View(proizvod);
                }
                proizvod.VremePripreme = time.Ticks;
                db.Proizvods.Add(proizvod);
                db.SaveChanges();

                // Dodavanje sastojaka
                for(int i = 0; i<sastojciProizvoda.Count; i++)
                {
                    sastojciProizvoda[i].Proizvod = proizvod.Id;
                }
                db.Sastojaks.AddRange(sastojciProizvoda);
                // save
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvod);
        }

        // GET: Proizvods/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            proizvod.Vreme = TimeSpan.FromTicks(proizvod.VremePripreme).ToString(@"hh\:mm");
            proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
            return View(proizvod);
        }

        // POST: Proizvods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Vreme,VremePripreme")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                if (db.Proizvods.Any(x => x.Id != proizvod.Id && x.Naziv.Equals(proizvod.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Proizvod sa unetim nazivom već postoji.");
                    return View(proizvod);
                }

                if (proizvod.Vreme.Length < 5)
                {
                    ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
                    return View(proizvod);
                }

                // => Split passed string
                string[] timeSplit = null;
                TimeSpan time = new TimeSpan();
                try
                {
                    // => Split passed string
                    timeSplit = proizvod.Vreme.Split(':');
                    time = new TimeSpan(int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
                    return View(proizvod);
                }
                proizvod.VremePripreme = time.Ticks;
                db.Entry(proizvod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvod);
        }

        // GET: Proizvods/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Proizvod proizvod = db.Proizvods.Find(id);
            // => Check if proizvod is in use
            if (db.VremenaProizvodnjes.Any(x => x.Proizvod.HasValue && x.Proizvod.Value == proizvod.Id)
                || db.Sastojaks.Any(x => x.Proizvod.HasValue && x.Proizvod.Value == proizvod.Id))
            {
                ModelState.AddModelError("Naziv", "Proizvod je u upotrebi.");
                return View(proizvod);
            }
            db.Proizvods.Remove(proizvod);
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
        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            return PartialView("_Sastojci");
        }

        public ActionResult DisplayNewSastojak()
        {
            return PartialView("_Sastojci", db.Materijals.OrderBy(x => x.Naziv).ToList());
        }

    }
}
;