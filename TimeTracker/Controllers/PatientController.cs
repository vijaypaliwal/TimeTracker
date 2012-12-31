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
    public class PatientController : BootstrapBaseController
    {
        private TimeTrackerContext db = new TimeTrackerContext();

        //
        // GET: /Patient/

        public ViewResult Index()
        {
            return View(db.Patients.ToList());
        }

        //
        // GET: /Patient/Details/5

        public ViewResult Details(int id)
        {
            Patient patient = db.Patients.Find(id);
            return View(patient);
        }

        //
        // GET: /Patient/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Patient/Create

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                Success("Patient Created Successfully");
                return RedirectToAction("Index");  
            }

            return View(patient);
        }
        
        //
        // GET: /Patient/Edit/5
 
        public ActionResult Edit(int id)
        {
            Patient patient = db.Patients.Find(id);
            return View(patient);
        }

        //
        // POST: /Patient/Edit/5

        [HttpPost]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                Success("Patient updated successfully");
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        //
        // GET: /Patient/Delete/5
 
        public ActionResult Delete(int id)
        {
            Patient patient = db.Patients.Find(id);
            return View(patient);
        }

        //
        // POST: /Patient/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            Information("Patient deleted successfully");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}