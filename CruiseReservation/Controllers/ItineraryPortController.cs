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
    public class ItineraryPortController : Controller
    {
        private CruiseReservationDB db = new CruiseReservationDB();

        // GET: ItineraryPort
        public ActionResult Index()
        {
            var itinerary_port = db.itinerary_port.Include(i => i.itinerary).Include(i => i.port);
            return View(itinerary_port.ToList());
        }

        // GET: ItineraryPort/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = (from i in db.itinerary
                          join ip in db.itinerary_port
                          on i.itinerary_id equals ip.itinerary_id
                          join p in db.port
                          on ip.port_id equals p.port_id
                          where ip.itinerary_id == id
                          select new itineraryPortViewModel
                          {
                              port_id = ip.port_id,
                              arrival_date_time = ip.arrival_date_time,
                              departure_date_time = ip.departure_date_time,
                              port_name = p.port_name,
                              itinerary_id = ip.itinerary_id,
                              itinerary_title = i.itinerary_title

                          }).ToList();



            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: ItineraryPort/Create
        public ActionResult Create()
        {
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title");
            ViewBag.port_id = new SelectList(db.port, "port_id", "port_name");
            return View();
        }

        // POST: ItineraryPort/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itinerary_port_id,itinerary_id,port_id,arrival_date_time,departure_date_time")] itinerary_port itinerary_port)
        {
            if (ModelState.IsValid)
            {
                db.itinerary_port.Add(itinerary_port);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title", itinerary_port.itinerary_id);
            ViewBag.port_id = new SelectList(db.port, "port_id", "port_name", itinerary_port.port_id);
            return View(itinerary_port);
        }

        // GET: ItineraryPort/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itinerary_port itinerary_port = db.itinerary_port.Find(id);
            if (itinerary_port == null)
            {
                return HttpNotFound();
            }
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title", itinerary_port.itinerary_id);
            ViewBag.port_id = new SelectList(db.port, "port_id", "port_name", itinerary_port.port_id);
            return View(itinerary_port);
        }

        // POST: ItineraryPort/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "itinerary_port_id,itinerary_id,port_id,arrival_date_time,departure_date_time")] itinerary_port itinerary_port)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itinerary_port).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.itinerary_id = new SelectList(db.itinerary, "itinerary_id", "itinerary_title", itinerary_port.itinerary_id);
            ViewBag.port_id = new SelectList(db.port, "port_id", "port_name", itinerary_port.port_id);
            return View(itinerary_port);
        }

        // GET: ItineraryPort/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itinerary_port itinerary_port = db.itinerary_port.Find(id);
            if (itinerary_port == null)
            {
                return HttpNotFound();
            }
            return View(itinerary_port);
        }

        // POST: ItineraryPort/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            itinerary_port itinerary_port = db.itinerary_port.Find(id);
            db.itinerary_port.Remove(itinerary_port);
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
