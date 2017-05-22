using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sw_New_mvc.Models;

namespace Sw_New_mvc.Controllers
{
    public class ECCEformController : Controller
    {
        private Con db = new Con();

        //
        // GET: /ECCEform/

        public ActionResult Index()
        {
            return View(db.icdsasMonthlyECCEDays.ToList());
        }

        //
        // GET: /ECCEform/Details/5

        public ActionResult Details(int id = 0)
        {
            icdsasMonthlyECCEDay icdsasmonthlyecceday = db.icdsasMonthlyECCEDays.Single(i => i.id == id);
            if (icdsasmonthlyecceday == null)
            {
                return HttpNotFound();
            }
            return View(icdsasmonthlyecceday);
        }

        //
        // GET: /ECCEform/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ECCEform/Create

        [HttpPost]
        public ActionResult Create(icdsasMonthlyECCEDay icdsasmonthlyecceday)
        {
            if (ModelState.IsValid)
            {
                db.icdsasMonthlyECCEDays.AddObject(icdsasmonthlyecceday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(icdsasmonthlyecceday);
        }

        //
        // GET: /ECCEform/Edit/5

        public ActionResult Edit(int id = 0)
        {
            icdsasMonthlyECCEDay icdsasmonthlyecceday = db.icdsasMonthlyECCEDays.Single(i => i.id == id);
            if (icdsasmonthlyecceday == null)
            {
                return HttpNotFound();
            }
            return View(icdsasmonthlyecceday);
        }

        //
        // POST: /ECCEform/Edit/5

        [HttpPost]
        public ActionResult Edit(icdsasMonthlyECCEDay icdsasmonthlyecceday)
        {
            if (ModelState.IsValid)
            {
                db.icdsasMonthlyECCEDays.Attach(icdsasmonthlyecceday);
                db.ObjectStateManager.ChangeObjectState(icdsasmonthlyecceday, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(icdsasmonthlyecceday);
        }

        //
        // GET: /ECCEform/Delete/5

        public ActionResult Delete(int id = 0)
        {
            icdsasMonthlyECCEDay icdsasmonthlyecceday = db.icdsasMonthlyECCEDays.Single(i => i.id == id);
            if (icdsasmonthlyecceday == null)
            {
                return HttpNotFound();
            }
            return View(icdsasmonthlyecceday);
        }

        //
        // POST: /ECCEform/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            icdsasMonthlyECCEDay icdsasmonthlyecceday = db.icdsasMonthlyECCEDays.Single(i => i.id == id);
            db.icdsasMonthlyECCEDays.DeleteObject(icdsasmonthlyecceday);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}