using Sell_Train_Ticket.Models;
using Sell_Train_Ticket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Sell_Train_Ticket.Controllers
{
    public class TripController : Controller
    {
        private ApplicationDbContext _context;

        public TripController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var trips = _context.Trips.Include("Route").Include("Train").ToList();

            return View(trips);
        }

        public ActionResult Add()
        {
            var viewModel = new TripViewModel
            {
                Trip = new Trip(),
                Routes = _context.Routes.ToList(),
                Trains = _context.Trains.ToList()
            };

            return View("Save", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var trip = _context.Trips.SingleOrDefault(t => t.Id == id);
            if (trip == null)
                return HttpNotFound();

            var viewModel = new TripViewModel
            {
                Trip = trip,
                Routes = _context.Routes.ToList(),
                Trains = _context.Trains.ToList()
            };

            return View("Save", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Trip trip)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new TripViewModel
                {
                    Trip = new Trip(),
                    Routes = _context.Routes.ToList(),
                    Trains = _context.Trains.ToList()
                };

                return View(viewModel);
            }
            if (trip.Id == 0)
            {
                _context.Trips.Add(trip);

                //Create a list of tickets for this trip
                var firstStation = _context.Stations.Single(s => s.RouteId == trip.RouteId && s.IsFirst == true);
                var finalStation = _context.Stations.Single(s => s.RouteId == trip.RouteId && s.IsFinal == true);
                var seats = _context.Seats.Where(s => s.Wagon.TrainId == trip.TrainId).ToList();
                foreach(var seat in seats)
                {
                    var newTicket = new Ticket();
                    newTicket.CustomerId = User.Identity.GetUserId();
                    newTicket.TripId = trip.Id;
                    newTicket.DepartureStationId = firstStation.Id;
                    newTicket.DestinationStationId = finalStation.Id;
                    newTicket.SeatId = seat.Id;
                    newTicket.IsKid = false;
                    newTicket.State = false;

                    _context.Tickets.Add(newTicket);
                }

                //Create a statistic for this new trip
                var tripStatistic = new TripStatistic
                {
                    TripId = trip.Id,
                    Revenue = 0,
                    TicketInStock = seats.Count()
                };

                _context.TripStatistics.Add(tripStatistic);
            }
            else
            {
                var tripInDb = _context.Trips.Single(t => t.Id == trip.Id);

                tripInDb.RouteId = trip.RouteId;
                tripInDb.DepartureDate = trip.DepartureDate;
                tripInDb.DepartureTime = trip.DepartureTime;
                tripInDb.ArrivalTime = trip.ArrivalTime;
                tripInDb.TrainId = trip.TrainId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Trip");
        }

        public ActionResult Delete(int id)
        {
            var tripInDb = _context.Trips.SingleOrDefault(t => t.Id == id);
            if (tripInDb == null)
            {
                return HttpNotFound();
            }

            _context.Trips.Remove(tripInDb);
            //Remove all tickets of removed trip
            var tickets = _context.Tickets.Where(t => t.TripId == tripInDb.Id).ToList();
            foreach(var ticket in tickets)
            {
                _context.Tickets.Remove(ticket);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Trip");
        }
    }
}