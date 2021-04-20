using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MRP.Models;

namespace MRP.Controllers
{
    public class RadnoMestoesController : Controller
    {
        private mrpEntities db = new mrpEntities();

        // GET: RadnoMestoes
        public ActionResult Index()
        {
            return View(db.RadnoMestoes.OrderBy(x=> x.Naziv).ToList());
        }

        // GET: RadnoMestoes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadnoMesto radnoMesto = db.RadnoMestoes.Find(id);
            if (radnoMesto == null)
            {
                return HttpNotFound();
            }
            return View(radnoMesto);
        }

        // GET: RadnoMestoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RadnoMestoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] RadnoMesto radnoMesto)
        {
            if (ModelState.IsValid)
            {
                // => Check if name is in use
                if (db.RadnoMestoes.Any(x => x.Naziv.Equals(radnoMesto.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Radno mesto sa unetim nazivom već postoji.");
                    return View(radnoMesto);
                }
                db.RadnoMestoes.Add(radnoMesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(radnoMesto);
        }

        // GET: RadnoMestoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadnoMesto radnoMesto = db.RadnoMestoes.Find(id);
            if (radnoMesto == null)
            {
                return HttpNotFound();
            }
            return View(radnoMesto);
        }

        // POST: RadnoMestoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] RadnoMesto radnoMesto)
        {
            if (ModelState.IsValid)
            {
                // => Check if new name is in use
                if(db.RadnoMestoes.Any(x=>x.Id != radnoMesto.Id && x.Naziv.Equals(radnoMesto.Naziv)))
                {
                    ModelState.AddModelError("Naziv", "Radno mesto sa unetim nazivom već postoji.");
                    return View(radnoMesto);
                }
                db.Entry(radnoMesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(radnoMesto);
        }

        // GET: RadnoMestoes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadnoMesto radnoMesto = db.RadnoMestoes.Find(id);
            if (radnoMesto == null)
            {
                return HttpNotFound();
            }
            return View(radnoMesto);
        }

        // POST: RadnoMestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RadnoMesto radnoMesto = db.RadnoMestoes.Find(id);
            // => Check if radno mesto is in use
            if (db.VremenaProizvodnjes.Any(x => x.RadnoMesto.HasValue && x.RadnoMesto.Value == radnoMesto.Id)
                || db.Zaposlenis.Any(x=>x.RadnoMesto.HasValue && x.RadnoMesto.Value == radnoMesto.Id))
            {
                ModelState.AddModelError("Naziv", "Radno mesto je u upotrebi.");
                return View(radnoMesto);
            }
            db.RadnoMestoes.Remove(radnoMesto);
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
