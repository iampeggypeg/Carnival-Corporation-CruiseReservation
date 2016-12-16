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
    public class CruiseController : Controller
    {
        private CruiseReservationDB db = new CruiseReservationDB();

        // GET: Cruise
        public ActionResult Index()
        {
            return View(db.cruise.ToList());
        }

        // GET: Itinerary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          from s in i.ship
                          where i.itinerary_id == id
                          select new itineraryViewModel
                          {
                              itinerary_id = i.itinerary_id,
                              itinerary_title = i.itinerary_title,
                              start_date = i.start_date,
                              end_date = i.end_date,
                              cruise_price = i.cruise_price,
                              ship_name = s.ship_name,
                          }).FirstOrDefault();



            if (result == null)
            {
                return HttpNotFound();
            }


            return View(result);
        }

        // GET: Cruise/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cruise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cruise_id,cruise_name,description")] cruise cruise)
        {
            if (ModelState.IsValid)
            {
                db.cruise.Add(cruise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cruise);
        }

        // GET: Cruise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cruise cruise = db.cruise.Find(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // POST: Cruise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cruise_id,cruise_name,description")] cruise cruise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cruise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cruise);
        }

        // GET: Cruise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cruise cruise = db.cruise.Find(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // POST: Cruise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cruise cruise = db.cruise.Find(id);
            db.cruise.Remove(cruise);
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
