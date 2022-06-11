using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TimeBetweenStationController : Controller
    {
        private ApplicationDbContext _context;

        public TimeBetweenStationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var tbs = _context.TimeBetweenStations
                .Include("FirstStation")
                .Include("SecondStation")
                .ToList();

            return View();
        }

        public ActionResult Add()
        {
            var route = new Models.Route();

            return View("Save", route);
        }

        public ActionResult Edit(int id)
        {
            var route = _context.Routes.SingleOrDefault(r => r.Id == id);
            if (route == null)
                return HttpNotFound();

            return View("Save", route);
        }

        [HttpPost]
        public ActionResult Save(Models.Route route)
        {
            if (!ModelState.IsValid)
            {
                var newRoute = new Models.Route();

                return View(newRoute);
            }
            if (route.Id == 0)
            {
                _context.Routes.Add(route);
            }
            else
            {
                var routeInDb = _context.Routes.Single(r => r.Id == route.Id);
                routeInDb.Name = route.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Route");
        }

        public ActionResult Delete(int id)
        {
            var routeInDb = _context.Routes.SingleOrDefault(r => r.Id == id);
            if (routeInDb == null)
            {
                return HttpNotFound();
            }

            //Delete tickets and trips belong to deleted route
            var trips = _context.Trips.Where(t => t.RouteId == id).ToList();

            foreach (var trip in trips)
            {
                var ticket = _context.Tickets.Single(t => t.TripId == trip.Id);
                _context.Tickets.Remove(ticket);
                _context.Trips.Remove(trip);
            }

            //Delete stations belong to deleted route
            var stations = _context.Stations.Where(s => s.RouteId == id).ToList();
            foreach (var station in stations)
            {
                _context.Stations.Remove(station);
            }
            //Delete route
            _context.Routes.Remove(routeInDb);

            _context.SaveChanges();

            return RedirectToAction("Index", "Route");
        }
    }
}