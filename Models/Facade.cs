using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Facade
    {
        private ApplicationDbContext _context;

        public Facade(ApplicationDbContext context)
        {
            _context = context;
        }

        //Delete Route and Stations, Trips, Tickets are dependent on the Route
        public void DeleteRoute(int routeId)
        {
            //Delete Route
            var route = _context.Routes.Single(r => r.Id == routeId);
            route.Delete(_context);

            //Delete Stations
            var stations = _context.Stations.Where(s => s.RouteId == route.Id).ToList();
            foreach (var station in stations)
                station.Delete(_context);

            //Delete Trips
            var trips = _context.Trips.Where(t => t.RouteId == route.Id).ToList();
            foreach(var trip in trips)
            {
                //Delete Tickets
                var tickets = _context.Tickets.Where(t => t.TripId == trip.Id).ToList();
                foreach (var ticket in tickets)
                    ticket.Delete(_context);

                trip.Delete(_context);
            }

            _context.SaveChanges();
        }
    }
}