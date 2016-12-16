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
    public class ShipCabinController : Controller
    {
        private CruiseReservationDB db = new CruiseReservationDB();

        // GET: ShipCabin
        public ActionResult Index(int? id, int sender)
        {
            //var customer_itinerary = db.customer_itinerary.Include(c => c.cabin).Include(c => c.itinerary);
            //return View(customer_itinerary.ToList());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          from si in i.ship
                          from cruise in si.cruise
                          from sc in si.ship_cabin
                          join c in db.cabin
                          on sc.cabin_id equals c.cabin_id
                          where sc.cabin_id == id
                          where i.itinerary_id == sender
                          select new customerItineraryViewModel
                          {
                              itinerary_title = i.itinerary_title,
                              start_date = i.start_date,
                              end_date = i.end_date,
                              cabin = c.cabin_name,
                              tour_price = i.cruise_price,
                              cabin_price = sc.price,
                              ship = si.ship_name,
                              cruise = cruise.cruise_name
                          }).FirstOrDefault();

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: ShipCabin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          from s in i.ship
                          join sc in db.ship_cabin
                          on s.ship_id equals sc.ship_id
                          join c in db.cabin
                          on sc.cabin_id equals c.cabin_id
                          where i.itinerary_id == id
                          select new shipCabinViewModel
                          {
                              ship_id = s.ship_id,
                              ship_name = s.ship_name,
                              cabin_id = c.cabin_id,
                              cabin_name = c.cabin_name,
                              no_of_cabin = sc.no_of_cabin,
                              maximum_occupant = sc.maximum_occupant,
                              price = sc.price,
                              itinerary_id = i.itinerary_id,
                              itinerary_title = i.itinerary_title,
                              itinerary_date = i.start_date
                          }).ToList();


            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: ShipCabin/Create
        public ActionResult Create()
        {
            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name");
            ViewBag.ship_id = new SelectList(db.ship, "ship_id", "ship_name");
            return View();
        }

        // POST: ShipCabin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ship_id,cabin_id,no_of_cabin,maximum_occupant,price")] ship_cabin ship_cabin)
        {
            if (ModelState.IsValid)
            {
                db.ship_cabin.Add(ship_cabin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name", ship_cabin.cabin_id);
            ViewBag.ship_id = new SelectList(db.ship, "ship_id", "ship_name", ship_cabin.ship_id);
            return View(ship_cabin);
        }

        // GET: ShipCabin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship_cabin ship_cabin = db.ship_cabin.Find(id);
            if (ship_cabin == null)
            {
                return HttpNotFound();
            }
            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name", ship_cabin.cabin_id);
            ViewBag.ship_id = new SelectList(db.ship, "ship_id", "ship_name", ship_cabin.ship_id);
            return View(ship_cabin);
        }

        // POST: ShipCabin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ship_id,cabin_id,no_of_cabin,maximum_occupant,price")] ship_cabin ship_cabin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ship_cabin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name", ship_cabin.cabin_id);
            ViewBag.ship_id = new SelectList(db.ship, "ship_id", "ship_name", ship_cabin.ship_id);
            return View(ship_cabin);
        }

        // GET: ShipCabin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship_cabin ship_cabin = db.ship_cabin.Find(id);
            if (ship_cabin == null)
            {
                return HttpNotFound();
            }
            return View(ship_cabin);
        }

        // POST: ShipCabin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ship_cabin ship_cabin = db.ship_cabin.Find(id);
            db.ship_cabin.Remove(ship_cabin);
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
