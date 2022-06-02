using Sell_Train_Ticket.Models;
using Sell_Train_Ticket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    public class TicketController : Controller
    {
        private ApplicationDbContext _context;

        public TicketController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var stationList = _context.Stations.ToList();
            var viewModel = new FindTripViewModel
            {
                DepartureStation = new Station(),
                DestinationStation = new Station(),
                DepartureDate = new DateTime(),
                Stations = stationList
            };

            return View(viewModel);
        }

        //Find trips that satisfy customer's demand (DepatureStation, DestinationStaion, 
        //DeparutreDate)
        public ActionResult FindTrip(FindTripViewModel viewModel)
        {
            //If departure station and destination station have different Route, return 
            //to Index page and annouce
            if (viewModel.DepartureStation.RouteId != viewModel.DestinationStation.RouteId)
                return View("Index", viewModel);

            int routeId = viewModel.DestinationStation.RouteId;
            var trips = _context.Trips.Where(t => t.RouteId == routeId).ToList();
            var tripsHasTicket = new List<Trip>();
            //Check whether exist trips has departure date satisfy customer's DepartureDate
            foreach (var trip in trips)
            {
                if (DateTime.Compare(trip.DepartureDate, viewModel.DepartureDate) == 0)
                    tripsHasTicket.Add(trip);
            }

            //If no trips founded, return to Index page and announce
            if (tripsHasTicket.Count < 1)
                return View("Index", viewModel);

            var findTicketViewModel = new FindTicketViewModel
            {
                DepartureStation = viewModel.DepartureStation,
                DestinationStation = viewModel.DestinationStation,
                Trips = tripsHasTicket
            };

            return View(findTicketViewModel);            
        }

        public ActionResult FindTicket(int id, Station depSta, Station desSta)
        {
            var tickets = _context.Tickets.Where(t => t.TripId == id).ToList();
            foreach(var ticket in tickets)
            {
                ticket.DepartureStationId = depSta.Id;
                //ticket.DepartureStation = depSta;
                ticket.DestinationStationId = desSta.Id;
                //ticket.DestinationStation = desSta;
            }

            return View(tickets);
        }
    }
}