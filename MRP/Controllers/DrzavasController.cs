using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MRP.Models;
using Newtonsoft.Json;

namespace MRP.Controllers
{
    public class DrzavasController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: Drzavas
        public ActionResult Index()
        {
            return View(db.Drzavas.OrderBy(x=>x.Naziv).ToList());
        }

        // GET: Drzavas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drzava drzava = db.Drzavas.Find(id);
            if (drzava == null)
            {
                return HttpNotFound();
            }
            return View(drzava);
        }

        // GET: Drzavas/Create
        public ActionResult Create()
        {
            // Get all countries from external api
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://restcountries.eu/rest/v2/all").Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(result);
            Drzava drzava = new Drzava();
            drzava.DrzavaDropDown = countries.Select(x => x.Name).ToList();
            return View(drzava);
        }

        // POST: Drzavas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] Drzava drzava)
        {
            if (ModelState.IsValid)
            {
                // Check name
                if (string.IsNullOrEmpty(drzava.Naziv))
                {
                    ModelState.AddModelError("Naziv", "Ime drzave je obavezno polje.");
                }
                else
                {
                    if (db.Drzavas.Any(x => x.Naziv.Equals(drzava.Naziv)))
                    {
                        ModelState.AddModelError("Naziv", "Drzava sa unetim nazivom već postoji.");
                    }
                }
                if (!ModelState.IsValid)
                {
                    return View(drzava);
                }
                db.Drzavas.Add(drzava);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drzava);
        }

        // GET: Drzavas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drzava drzava = db.Drzavas.Find(id);
            // Get all countries from external api
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://restcountries.eu/rest/v2/all").Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(result);
            drzava.DrzavaDropDown = countries.Select(x => x.Name).ToList();
            if (drzava == null)
            {
                return HttpNotFound();
            }
            return View(drzava);
        }

        // POST: Drzavas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] Drzava drzava)
        {
            if (ModelState.IsValid)
            {
                // Check name
                if (string.IsNullOrEmpty(drzava.Naziv))
                {
                    ModelState.AddModelError("Naziv", "Ime drzave je obavezno polje.");
                }
                else
                {
                    if (db.Drzavas.Any(x => x.Id != drzava.Id && x.Naziv.Equals(drzava.Naziv)))
                    {
                        ModelState.AddModelError("Naziv", "Drzava sa unetim nazivom već postoji.");
                    }
                }
                if (!ModelState.IsValid)
                {
                    return View(drzava);
                }
                db.Entry(drzava).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drzava);
        }

        // GET: Drzavas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drzava drzava = db.Drzavas.Find(id);
            if (drzava == null)
            {
                return HttpNotFound();
            }
            return View(drzava);
        }

        // POST: Drzavas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Drzava drzava = db.Drzavas.Find(id);
            if (db.Dobavljacs.Any(x=> x.Drzava == id) || db.Zaposlenis.Any(x=> x.Drzava == id))
            {
                ModelState.AddModelError("Naziv", "Drzava u upotrebi.");
                return View(drzava);
            }

            db.Drzavas.Remove(drzava);
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
