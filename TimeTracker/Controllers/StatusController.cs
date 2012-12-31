using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{ 
    public class StatusController : BootstrapBaseController
    {
        private TimeTrackerContext db = new TimeTrackerContext();

        //
        // GET: /Status/

        public ViewResult Index()
        {
            return View(db.Status.ToList());
        }

        //
        // GET: /Status/Details/5

        public ViewResult Details(int id)
        {
            Status status = db.Status.Find(id);
            return View(status);
        }

        //
        // GET: /Status/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Status/Create

        [HttpPost]
        public ActionResult Create(Status status)
        {
            if (ModelState.IsValid)
            {
                db.Status.Add(status);
                db.SaveChanges();
                Success("Status Created Successfully");
                return RedirectToAction("Index");  
            }

            return View(status);
        }
        
        //
        // GET: /Status/Edit/5
 
        public ActionResult Edit(int id)
        {
            Status status = db.Status.Find(id);
            return View(status);
        }

        //
        // POST: /Status/Edit/5

        [HttpPost]
        public ActionResult Edit(Status status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                Success("Status updated successfully");
                return RedirectToAction("Index");
            }
            return View(status);
        }

        //
        // GET: /Status/Delete/5
 
        public ActionResult Delete(int id)
        {
            Status status = db.Status.Find(id);
            return View(status);
        }

        //
        // POST: /Status/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Status status = db.Status.Find(id);
            db.Status.Remove(status);
            db.SaveChanges();
            Information("Status deleted successfully");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}