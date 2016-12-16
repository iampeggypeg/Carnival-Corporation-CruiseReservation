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
    public class ItineraryController : Controller
    {
        private CruiseReservationDB db = new CruiseReservationDB();

        // GET: Itinerary
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var result = (from i in db.itinerary
                          from s in i.ship
                          from ip in i.itinerary_port
                          join p in db.port
                          on ip.port_id equals p.port_id
                          from dp in p.destination
                          where dp.destination_id == id
                          group i by new
                          {
                              i.itinerary_id,
                              i.itinerary_title,
                              i.start_date,
                              i.end_date,
                              i.cruise_price,
                              s.ship_name
                          } into itineraryGroup
                          select new itineraryViewModel
                          {
                              itinerary_id = itineraryGroup.Key.itinerary_id,
                              itinerary_title = itineraryGroup.Key.itinerary_title,
                              start_date = itineraryGroup.Key.start_date,
                              end_date = itineraryGroup.Key.end_date,
                              cruise_price = itineraryGroup.Key.cruise_price,
                              ship_name = itineraryGroup.Key.ship_name,
                          }).ToList();

            if (result == null)
            {
                return HttpNotFound();
            }


            return View(result);
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

        public ActionResult ShipItinerary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          from s in i.ship
                          where s.ship_id == id
                          select new itineraryViewModel
                          {
                              itinerary_id = i.itinerary_id,
                              itinerary_title = i.itinerary_title,
                              start_date = i.start_date,
                              end_date = i.end_date,
                              cruise_price = i.cruise_price,
                              ship_name = s.ship_name,
                          }).ToList();



            if (result == null)
            {
                return HttpNotFound();
            }


            return View(result);
        }


        // GET: Itinerary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Itinerary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itinerary_id,itinerary_title,start_date,end_date,cruise_price")] itinerary itinerary)
        {
            if (ModelState.IsValid)
            {
                db.itinerary.Add(itinerary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itinerary);
        }

        // GET: Itinerary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itinerary itinerary = db.itinerary.Find(id);
            if (itinerary == null)
            {
                return HttpNotFound();
            }
            return View(itinerary);
        }

        // POST: Itinerary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "itinerary_id,itinerary_title,start_date,end_date,cruise_price")] itinerary itinerary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itinerary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itinerary);
        }

        // GET: Itinerary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itinerary itinerary = db.itinerary.Find(id);
            if (itinerary == null)
            {
                return HttpNotFound();
            }
            return View(itinerary);
        }

        // POST: Itinerary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            itinerary itinerary = db.itinerary.Find(id);
            db.itinerary.Remove(itinerary);
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
