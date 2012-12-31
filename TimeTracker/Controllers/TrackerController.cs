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
    public class TrackerController : Controller
    {
        private TimeTrackerContext db = new TimeTrackerContext();

        //
        // GET: /Tracker/

        public ViewResult Index()
        {
            var trackers = db.Trackers.Include(t => t.Patient).Include(t => t.Status);
            return View(trackers.ToList());
        }

        //
        // GET: /Tracker/Details/5

        public ViewResult Details(int id)
        {
            Tracker tracker = db.Trackers.Find(id);
            return View(tracker);
        }

        public ActionResult TrackTime()
        {
            //ViewBag.PatientID = new SelectList(db.Patients, "Id", "FirstName");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name");
            return PartialView(new Tracker());
        }

        public JsonResult AllPatients(string pname)
        {
            var allPatients = db.Patients.Where(s => s.FirstName.Contains(pname) || s.MiddleName.Contains(pname) || s.LastName.Contains(pname));
            if (allPatients == null)
            {
                var list=new List<SelectListItem>();
                list.Add(new SelectListItem() { Value="Not Found",Text="Not Found"});

                return Json(new SelectList(list, "Id", "FullName"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(allPatients, "Id", "FullName"), JsonRequestBehavior.AllowGet);
            }
        }

        public ContentResult GetPatientType(int pid)
        {
            var patinet=db.Patients.Where(p => p.Id == pid).FirstOrDefault();
            string returnstring = "<label class='label label-";
            returnstring += patinet.PersonType + "'>";
            returnstring += PersonType.AllTypes().Where(p => p.Value == patinet.PersonType).FirstOrDefault().Text + "</label>";
            return Content(returnstring);
        }


        [HttpPost]
        public ActionResult TrackTime(Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                db.Trackers.Add(tracker);
                db.SaveChanges();
                return Content("True", "text/html");
            }

            ViewBag.PatientID = new SelectList(db.Patients, "Id", "FirstName", tracker.PatientID);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", tracker.StatusId);
            return View(tracker);
        }
        //
        // GET: /Tracker/Create

        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "FirstName");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name");
            return View();
        } 

        //
        // POST: /Tracker/Create

        [HttpPost]
        public ActionResult Create(Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                db.Trackers.Add(tracker);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PatientID = new SelectList(db.Patients, "Id", "FirstName", tracker.PatientID);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", tracker.StatusId);
            return View(tracker);
        }
        
        //
        // GET: /Tracker/Edit/5
 
        public ActionResult Edit(int id)
        {
            Tracker tracker = db.Trackers.Find(id);
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "FirstName", tracker.PatientID);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", tracker.StatusId);
            return View(tracker);
        }

        //
        // POST: /Tracker/Edit/5

        [HttpPost]
        public ActionResult Edit(Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tracker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "FirstName", tracker.PatientID);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", tracker.StatusId);
            return View(tracker);
        }

        //
        // GET: /Tracker/Delete/5
 
        public ActionResult Delete(int id)
        {
            Tracker tracker = db.Trackers.Find(id);
            return View(tracker);
        }

        //
        // POST: /Tracker/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Tracker tracker = db.Trackers.Find(id);
            db.Trackers.Remove(tracker);
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