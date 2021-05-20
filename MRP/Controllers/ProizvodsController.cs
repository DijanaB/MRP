using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
            proizvod.Sastojaks = db.Sastojaks.Where(x => x.Proizvod == id).ToList();
            foreach (var sastojak in proizvod.Sastojaks)
            {
                sastojak.Materijali = proizvod.Materijali;
            }
            return View(proizvod);
        }

        // GET: Proizvods/Create
        public ActionResult Create()
        {
            Proizvod proizvod = new Proizvod();
            proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
            proizvod.Sastojaks = new List<Sastojak>() { new Sastojak() };
            foreach (var sastojak in proizvod.Sastojaks)
            {
                sastojak.Materijali = proizvod.Materijali;
            }
            return View(proizvod);
        }

        // POST: Proizvods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,VremePripreme,Vreme,Sastojaks")] Proizvod proizvod, FormCollection collection)
        {
            // Check name
            if (string.IsNullOrEmpty(proizvod.Naziv))
            {
                ModelState.AddModelError("Naziv", "Ime proizvoda je obavezno polje.");
            }
            else
            {
                if (db.Proizvods.Any(x => x.Naziv.Equals(proizvod.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Proizvod sa unetim nazivom već postoji.");
                }
            }
               
            TimeSpan time = new TimeSpan();
            // Check time
            if (proizvod.Vreme == null || proizvod.Vreme.Length < 5)
            {
                ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
            }
            else
            {
                // => Split passed string
                string[] timeSplit = null;
                try
                {
                    // => Split passed string
                    timeSplit = proizvod.Vreme.Split(':');
                    time = new TimeSpan(int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
                }
            }

            // sastojci
            string[] sastojci = collection["Materijal"].ToString().Split(',');
            string[] kolicina = collection["Kolicina"].ToString().Split(',');
            List<Sastojak> sastojciProizvoda = new List<Sastojak>();
            for (int i = 0; i < sastojci.Length; i++)
            {
                int id = int.Parse(sastojci[i]);
                float kol;
                try
                {   
                    kol = float.Parse(kolicina[i]);
                }
                catch 
                {
                    kol = 0;
                }

                Sastojak sastojak = new Sastojak
                {
                    Materijal = id,
                    Kolicina = kol
                };
                if(sastojak.Kolicina == 0)
                {
                    ModelState.AddModelError("Materijali", "Sastojci nisu dobro uneti.");
                }
                else
                {
                    if (sastojciProizvoda.Any(x => x.Materijal == sastojak.Materijal))
                    {
                        ModelState.AddModelError("Materijali", "Odabrana su dva ista sastojka.");
                    }
                }
                sastojciProizvoda.Add(sastojak);
            }

            if (!sastojciProizvoda.Any())
            {
                ModelState.AddModelError("Materijali", "Sastojci su obavezni.");
            }
            // If there are errors, return current proizvod
            if (!ModelState.IsValid)
            {
                proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                proizvod.Sastojaks = sastojciProizvoda;
                foreach (var sastojak in proizvod.Sastojaks)
                {
                    sastojak.Materijali = proizvod.Materijali;
                }
                return View(proizvod);
            }

            proizvod.VremePripreme = time.Ticks;
            db.Proizvods.Add(proizvod);
            db.SaveChanges();

            // Dodavanje sastojaka
            for (int i = 0; i < sastojciProizvoda.Count; i++)
            {
                sastojciProizvoda[i].Proizvod = proizvod.Id;
            }
            db.Sastojaks.AddRange(sastojciProizvoda);
            // save
            db.SaveChanges();
            return RedirectToAction("Index");
            
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
            proizvod.Sastojaks = db.Sastojaks.Where(x => x.Proizvod == id).ToList();
            foreach (var sastojak in proizvod.Sastojaks)
            {
                sastojak.Materijali = proizvod.Materijali;
            }
            return View(proizvod);
        }

        // POST: Proizvods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Vreme,VremePripreme")] Proizvod proizvod, FormCollection collection)
        {
            // Check name
            if (string.IsNullOrEmpty(proizvod.Naziv))
            {
                ModelState.AddModelError("Naziv", "Ime proizvoda je obavezno polje.");
            }
            else
            {
                if (db.Proizvods.Any(x => x.Id != proizvod.Id && x.Naziv.Equals(proizvod.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Proizvod sa unetim nazivom već postoji.");
                }
            }

            TimeSpan time = new TimeSpan();
            // Check time
            if (proizvod.Vreme == null || proizvod.Vreme.Length < 5)
            {
                ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
            }
            else
            {
                // => Split passed string
                string[] timeSplit = null;
                try
                {
                    // => Split passed string
                    timeSplit = proizvod.Vreme.Split(':');
                    time = new TimeSpan(int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vreme", "Vreme mora biti uneto u formatu 00:00");
                }
            }

            // sastojci
            string[] sastojci = collection["Materijal"].ToString().Split(',');
            string[] kolicina = collection["Kolicina"].ToString().Split(',');
            List<Sastojak> sastojciProizvoda = new List<Sastojak>();
            for (int i = 0; i < sastojci.Length; i++)
            {
                int id = int.Parse(sastojci[i]);
                float kol;
                try
                {
                    kol = float.Parse(kolicina[i]);
                }
                catch
                {
                    kol = 0;
                }

                Sastojak sastojak = new Sastojak
                {
                    Materijal = id,
                    Kolicina = kol
                };
                if (sastojak.Kolicina == 0)
                {
                    ModelState.AddModelError("Materijali", "Sastojci nisu dobro uneti.");
                }
                else
                {
                    if (sastojciProizvoda.Any(x => x.Materijal == sastojak.Materijal))
                    {
                        ModelState.AddModelError("Materijali", "Odabrana su dva ista sastojka.");
                    }
                }
                sastojciProizvoda.Add(sastojak);
            }

            if (!sastojciProizvoda.Any())
            {
                ModelState.AddModelError("Materijali", "Sastojci su obavezni.");
            }
            // If there are errors, return current proizvod
            if (!ModelState.IsValid)
            {
                proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
                proizvod.Sastojaks = sastojciProizvoda;
                foreach (var sastojak in proizvod.Sastojaks)
                {
                    sastojak.Materijali = proizvod.Materijali;
                }
                return View(proizvod);
            }

            proizvod.VremePripreme = time.Ticks;
            db.Entry<Proizvod>(proizvod).State = EntityState.Modified;
            db.SaveChanges();
            List<Sastojak> sastojciDB = db.Sastojaks.Where(x => x.Proizvod == proizvod.Id).ToList();
            db.Sastojaks.RemoveRange(sastojciDB);
            // Dodavanje sastojaka
            for (int i = 0; i < sastojciProizvoda.Count; i++)
            {
                sastojciProizvoda[i].Proizvod = proizvod.Id;
            }
            db.Sastojaks.AddRange(sastojciProizvoda);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            proizvod.Vreme = TimeSpan.FromTicks(proizvod.VremePripreme).ToString(@"hh\:mm");
            proizvod.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
            proizvod.Sastojaks = db.Sastojaks.Where(x => x.Proizvod == id).ToList();
            foreach (var sastojak in proizvod.Sastojaks)
            {
                sastojak.Materijali = proizvod.Materijali;
            }
            return View(proizvod);
        }

        // POST: Proizvods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Proizvod proizvod = db.Proizvods.Find(id);
            List<Sastojak> sastojci = db.Sastojaks.Where(x => x.Proizvod == id).ToList();
            db.Sastojaks.RemoveRange(sastojci);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSastojak()
        {
            Sastojak sastojak = new Sastojak();
            sastojak.Materijali = db.Materijals.OrderBy(x => x.Naziv).ToList();
            return PartialView("Sastojak", sastojak);
        }


    }
}
;