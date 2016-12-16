using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CruiseReservation;

namespace CruiseReservation.Controllers
{
    public class DestinationController : Controller
    {
        private CruiseReservationDB db = new CruiseReservationDB();

        // GET: Destination
        public ActionResult Index()
        {
            return View(db.destination.ToList());
        }

        // GET: Destination/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from s in db.ship
                          from c in s.cruise
                          from si in s.itinerary
                          from ip in si.itinerary_port
                          join p in db.port
                          on ip.port_id equals p.port_id
                          from d in p.destination
                          where c.cruise_id == id
                          group d by new
                          {
                              d.destination_id,
                              d.destination_name

                          }into destinationGroup
                          select new destinationViewModel
                          {
                              destination_id = destinationGroup.Key.destination_id,
                              destination_name = destinationGroup.Key.destination_name
                          }).ToList();


            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Destination/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Destination/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "destination_id,destination_name")] destination destination)
        {
            if (ModelState.IsValid)
            {
                db.destination.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: Destination/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destination destination = db.destination.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Destination/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "destination_id,destination_name")] destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }

        // GET: Destination/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            destination destination = db.destination.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Destination/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            destination destination = db.destination.Find(id);
            db.destination.Remove(destination);
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
