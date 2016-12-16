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
    public class CustomerItineraryController : Controller
    {
        private CruiseReservationDB db = new CruiseReservationDB();
        
        [Authorize]
        // GET: CustomerItinerary
        public ActionResult Index (int? cabinID, int? itineraryID)
        {
            if (cabinID == null && itineraryID == null || cabinID == null || itineraryID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          from si in i.ship
                          from cruise in si.cruise
                          from sc in si.ship_cabin
                          join c in db.cabin
                          on sc.cabin_id equals c.cabin_id
                          where sc.cabin_id == cabinID
                          where i.itinerary_id == itineraryID
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

        public ActionResult ConfirmBooking(int? cabinID, int? itineraryID)
        {
            if (cabinID == null && itineraryID == null || cabinID == null || itineraryID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          from si in i.ship
                          from cruise in si.cruise
                          from sc in si.ship_cabin
                          join c in db.cabin
                          on sc.cabin_id equals c.cabin_id
                          where sc.cabin_id == cabinID
                          where i.itinerary_id == itineraryID
                          select new customerItineraryViewModel
                          {
                              itinerary_id = i.itinerary_id,
                              itinerary_title = i.itinerary_title,
                              start_date = i.start_date,
                              end_date = i.end_date,
                              cabin_id = c.cabin_id,
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

        public ActionResult Confirm(customerItineraryViewModel model)
        {
            //var customerItinerary = new customerItineraryConfirmModel()
            //{
            //    customer_id = User.Identity.Name,
            //    itinerary_id = model.itinerary_id,
            //    cabin_id = model.cabin_id,
            //    no_of_pax = 1
            //};
            customer_itinerary itinerary = new customer_itinerary();
            itinerary.customer_id = User.Identity.Name;
            itinerary.itinerary_id = model.itinerary_id;
            itinerary.cabin_id = model.cabin_id;
            itinerary.no_of_pax = 1;

            try
            {
                db.customer_itinerary.Add(itinerary);
                db.SaveChanges();

            }

            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }


            return RedirectToAction("Details", new System.Web.Routing.RouteValueDictionary(
            new { controller = "CustomerItinerary", action = "Details"}));
        }
        

        // GET: CustomerItinerary/Details/5
        public ActionResult Details()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //customer_itinerary customer_itinerary = db.customer_itinerary.Find(id);
            //if (customer_itinerary == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(customer_itinerary);

            var result = (from ci in db.customer_itinerary
                          join i in db.itinerary
                          on ci.itinerary_id equals i.itinerary_id
                          join c in db.cabin
                          on ci.cabin_id equals c.cabin_id
                          where ci.customer_id == User.Identity.Name
                          select new customerItineraryBookingDetailsModel
                          {
                              itinerary_id = i.itinerary_id,
                              itinerary_title = i.itinerary_title,
                              cabin_id = c.cabin_id,
                              cabin = c.cabin_name,
                              no_of_pax = ci.no_of_pax
                          }).ToList();



            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);


        }

        // GET: CustomerItinerary/Create
        public ActionResult Create()
        {
            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name");
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title");
            return View();
        }

        // POST: CustomerItinerary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_itinerary_id,customer_id,itinerary_id,no_of_pax,cabin_id")] customer_itinerary customer_itinerary)
        {
            if (ModelState.IsValid)
            {
                db.customer_itinerary.Add(customer_itinerary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name", customer_itinerary.cabin_id);
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title", customer_itinerary.itinerary_id);
            return View(customer_itinerary);
        }

        // GET: CustomerItinerary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_itinerary customer_itinerary = db.customer_itinerary.Find(id);
            if (customer_itinerary == null)
            {
                return HttpNotFound();
            }
            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name", customer_itinerary.cabin_id);
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title", customer_itinerary.itinerary_id);
            return View(customer_itinerary);
        }

        // POST: CustomerItinerary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customer_itinerary_id,customer_id,itinerary_id,no_of_pax,cabin_id")] customer_itinerary customer_itinerary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_itinerary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cabin_id = new SelectList(db.cabin, "cabin_id", "cabin_name", customer_itinerary.cabin_id);
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title", customer_itinerary.itinerary_id);
            return View(customer_itinerary);
        }

        // GET: CustomerItinerary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_itinerary customer_itinerary = db.customer_itinerary.Find(id);
            if (customer_itinerary == null)
            {
                return HttpNotFound();
            }
            return View(customer_itinerary);
        }

        // POST: CustomerItinerary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer_itinerary customer_itinerary = db.customer_itinerary.Find(id);
            db.customer_itinerary.Remove(customer_itinerary);
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
