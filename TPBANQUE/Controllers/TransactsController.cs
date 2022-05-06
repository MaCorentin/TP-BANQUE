using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ORM;

namespace TPBANQUE.Controllers
{
    public class TransactsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Transacts
        public async Task<ActionResult> Index()
        {
            var transacts = db.Transacts.Include(t => t.Compte);
            return View(await transacts.ToListAsync());
        }

        // GET: Transacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transact transact = await db.Transacts.FindAsync(id);
            if (transact == null)
            {
                return HttpNotFound();
            }
            return View(transact);
        }

        // GET: Transacts/Create
        public ActionResult Create()
        {
            ViewBag.IdCompte = new SelectList(db.Comptes, "IdCompte", "Libelle");
            return View();
        }

        // POST: Transacts/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTransaction,TypeTransaction,DateTransact,Montant,IdCompte")] Transact transact)
        {
            if (ModelState.IsValid)
            {
                db.Transacts.Add(transact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdCompte = new SelectList(db.Comptes, "IdCompte", "Libelle", transact.IdCompte);
            return View(transact);
        }

        // GET: Transacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transact transact = await db.Transacts.FindAsync(id);
            if (transact == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCompte = new SelectList(db.Comptes, "IdCompte", "Libelle", transact.IdCompte);
            return View(transact);
        }

        // POST: Transacts/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTransaction,TypeTransaction,DateTransact,Montant,IdCompte")] Transact transact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdCompte = new SelectList(db.Comptes, "IdCompte", "Libelle", transact.IdCompte);
            return View(transact);
        }

        // GET: Transacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transact transact = await db.Transacts.FindAsync(id);
            if (transact == null)
            {
                return HttpNotFound();
            }
            return View(transact);
        }

        // POST: Transacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Transact transact = await db.Transacts.FindAsync(id);
            db.Transacts.Remove(transact);
            await db.SaveChangesAsync();
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
