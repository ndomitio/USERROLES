using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using USERROLES.Models;

namespace USERROLES.Controllers
{
    public class LogsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Logs
        public ActionResult Index()
        {
            var logs = db.Logs.Include(l => l.Patient).Include(l => l.Physician).Include(l => l.RX);
            return View(logs.ToList());
        }

        // GET: Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Logs/Create
        public ActionResult Create()
        {
            ViewBag.PID = new SelectList(db.Patients, "PID", "PFName");
            ViewBag.PhID = new SelectList(db.Physicians, "PhID", "PhFName");
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName");
            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogID,LogTimeStamp,PainNumb,DiffNumb,ComTxt,IsComplete,FullSet,PartialSet,PID,PhID,RxID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PID = new SelectList(db.Patients, "PID", "PFName", log.PID);
            ViewBag.PhID = new SelectList(db.Physicians, "PhID", "PhFName", log.PhID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", log.RxID);
            return View(log);
        }

        // GET: Logs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            ViewBag.PID = new SelectList(db.Patients, "PID", "PFName", log.PID);
            ViewBag.PhID = new SelectList(db.Physicians, "PhID", "PhFName", log.PhID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", log.RxID);
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogID,LogTimeStamp,PainNumb,DiffNumb,ComTxt,IsComplete,FullSet,PartialSet,PID,PhID,RxID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PID = new SelectList(db.Patients, "PID", "PFName", log.PID);
            ViewBag.PhID = new SelectList(db.Physicians, "PhID", "PhFName", log.PhID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", log.RxID);
            return View(log);
        }

        // GET: Logs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
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
