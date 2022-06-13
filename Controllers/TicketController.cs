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

        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            var tickets = _context.Tickets.
                Include("Customer").
                Include("Trip").
                Include("Trip.Route").
                Include("DepartureStation").
                Include("DestinationStation").
                Include("Seat").
                ToList();

            return View(tickets);
        }

        //Find trips that satisfy customer's demand (DepatureStation, DestinationStaion, 
        //DeparutreDate)
        public ActionResult FindTrip(FindTripViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var stations = _context.Stations.ToList();
                viewModel.Stations = stations;
                return View("/Views/Home/Index.cshtml", viewModel);
            }

            var depSta = _context.Stations.SingleOrDefault(s => s.Id == viewModel.DepartureStation);
            var desSta = _context.Stations.SingleOrDefault(s => s.Id == viewModel.DestinationStation);

            //If departure station and destination station have different Route, return 
            //to Index page and annouce
            if (depSta.RouteId != desSta.RouteId)
            {
                var stations = _context.Stations.ToList();
                viewModel.Stations = stations;
                viewModel.AnnounceMessage = "No trips founded!";
                return View("/Views/Home/Index.cshtml", viewModel);
            }
            //If in case departure station and destination station are the same
            if(depSta.Id == desSta.Id)
            {
                var stations = _context.Stations.ToList();
                viewModel.Stations = stations;
                viewModel.AnnounceMessage = "No trips founded!";
                return View("/Views/Home/Index.cshtml", viewModel);
            }
            //If in case customer go reverse trip
            if(depSta.Id > desSta.Id)
            {
                //Find reverse trips
                var reverseTrips = _context.Trips
                    .Include("Route")
                    .Where(t => t.RouteId == depSta.RouteId 
                    && t.IsReverse 
                    && DateTime.Compare(t.DepartureDate, viewModel.DepartureDate) == 0)
                    .ToList();

                //If no trips founded, return to Index page and announce
                if (reverseTrips.Count < 1)
                {
                    var stations = _context.Stations.ToList();
                    viewModel.Stations = stations;
                    viewModel.AnnounceMessage = "No trips founded!";
                    return View("/Views/Home/Index.cshtml", viewModel);
                }

                var _findTicketViewModel = new FindTicketViewModel
                {
                    DepartureStation = depSta,
                    DestinationStation = desSta,
                    Trips = reverseTrips
                };

                return View(_findTicketViewModel);
            }

            //If in case customer doesn't go reverse trip
            var trips = _context.Trips
                .Include("Route")
                .Where(t => t.RouteId == depSta.RouteId 
                && DateTime.Compare(t.DepartureDate, viewModel.DepartureDate) == 0)
                .ToList();

            //If no trips founded, return to Index page and announce
            if (trips.Count < 1)
            {
                var stations = _context.Stations.ToList();
                viewModel.Stations = stations;
                viewModel.AnnounceMessage = "No trips founded!";
                return View("/Views/Home/Index.cshtml", viewModel);
            }

            var findTicketViewModel = new FindTicketViewModel
            {
                DepartureStation = depSta,
                DestinationStation = desSta,
                DepartureDate = viewModel.DepartureDate,
                Trips = trips
            };

            return View(findTicketViewModel);            
        }

        public ActionResult FindTicket(int tripId, int depStaId, int desStaId, DateTime depDate)
        {
            var depSta = _context.Stations.SingleOrDefault(s => s.Id == depStaId);
            var desSta = _context.Stations.SingleOrDefault(s => s.Id == desStaId);
                    
            var tickets = _context.Tickets
                .Include("Trip")
                .Include("Seat")
                .Include("Seat.SeatType")
                .Include("Seat.Wagon")
                .Include("Seat.Wagon.Train")
                .Where(t => t.TripId == tripId)
                .ToList();
            if (tickets.Count() < 1)
                return HttpNotFound();

            //Calculate ticket price
            foreach(var ticket in tickets)
            {
                if (!ticket.State)
                {
                    var smallerStationId = ticket.DepartureStationId < ticket.DestinationStationId ? ticket.DepartureStationId : ticket.DestinationStationId;
                    var biggerStationId = ticket.DepartureStationId > ticket.DestinationStationId ? ticket.DepartureStationId : ticket.DestinationStationId;
                    var timeBetweenStations = _context.TimeBetweenStations
                        .Where(t => t.FirstStationId >= smallerStationId && t.SecondStationId <= biggerStationId);
                    var totalTravelTime = 0;
                    foreach (var item in timeBetweenStations)
                    {
                        totalTravelTime += item.MovingTime;
                    }

                    ticket.Price += (ticket.Seat.SeatType.Price + totalTravelTime * 3000);
                }
            }            

            var viewModel = new DisplayTicketViewModel
            {
                Tickets = tickets,
                DepartureStation = depSta,
                DestinationStation = desSta,
                DepartureDate = depDate
            };
            return View(viewModel);
        }

        public ActionResult BuyTicket(int ticketId,int ticketPrice, int depStaId, int desStaId)
        {
            var ticket = _context.Tickets.Single(t => t.Id == ticketId);
            var tripStatistic = _context.TripStatistics.Single(t => t.TripId == ticket.TripId);

            ticket.CustomerId = CurrentUserId.GetInstance().GetUserId();
            ticket.DepartureStationId = depStaId;
            ticket.DestinationStationId = desStaId;
            ticket.State = true;
            ticket.Price = ticketPrice;
            //ticket.Price += (ticket.Seat.SeatType.Price + totalTravelTime * 3000);
            tripStatistic.Revenue += ticket.Price;
            tripStatistic.TicketInStock--;

            _context.SaveChanges();

            return RedirectToAction("Refund", "Ticket");

        }

        public ActionResult Refund()
        {
            var currentUserId = CurrentUserId.GetInstance().GetUserId();
            var viewModel = new RefundViewModel
            {
                Tickets = _context.Tickets
                .Include("Trip")
                .Include("Trip.Route")
                .Include("DepartureStation")
                .Include("DestinationStation")
                .Include("Seat")
                .Include("Seat.SeatType")
                .Include("Seat.Wagon")
                .Include("Seat.Wagon.Train")
                .Where(t => t.CustomerId == currentUserId && t.State == true).ToList(),
                AnnounceMessage = ""
            };

            return View(viewModel);
        } 

        public ActionResult RefundTicket(int ticketId)
        {
            var ticket = _context.Tickets.Include("Trip").Include("Seat").Include("Seat.SeatType").SingleOrDefault(t => t.Id == ticketId);
            var tripStatistic = _context.TripStatistics.SingleOrDefault(t => t.TripId == ticket.TripId);
            int timeCompare = DateTime.Compare(ticket.Trip.DepartureDate.Date, DateTime.Now.Date);
            //If departure date is later than current date then refund the ticket 
            if (timeCompare > 0)
            {
                long tenPercentPrice = ticket.Price / 10;
                tripStatistic.Revenue -= (ticket.Price - tenPercentPrice);
                tripStatistic.TicketInStock++;
                RenewedTicket(ticket);

                _context.SaveChanges();

                return RedirectToAction("Refund", "Ticket");
            }
            //If departure date is equal to current date then compare time
            if (timeCompare == 0)
            {
                var departureTime = ticket.Trip.DepartureTime.TimeOfDay;
                var currentTime = DateTime.Now.TimeOfDay;
                var diff = departureTime - currentTime;
                //If the current time is sooner than 30m comapre to departure time then 
                //refund the ticket
                if(diff.TotalMinutes > 30)
                {
                    long tenPercentPrice = ticket.Price / 10;
                    tripStatistic.Revenue -= (ticket.Price - tenPercentPrice);
                    tripStatistic.TicketInStock++;
                    RenewedTicket(ticket);

                    _context.SaveChanges();

                    return RedirectToAction("Refund", "Ticket");
                }
            }

            var viewModel = new RefundViewModel
            {
                Tickets = _context.Tickets
                .Where(t => t.CustomerId == CurrentUserId.GetInstance().GetUserId() 
                && t.State == true)
                .ToList(),
                AnnounceMessage = "You are only able to refund ticket 30 minutes before the departure time"
            };

            return View("Refund", viewModel);
        }

        public void RenewedTicket(Ticket ticket)
        {
            var depSta = _context.Stations.Single(s => s.RouteId == ticket.Trip.RouteId && s.IsFirst == true);
            var desSta = _context.Stations.Single(s => s.RouteId == ticket.Trip.RouteId && s.IsFinal == true);

            ticket.CustomerId = _context.Users.Single(u => u.FullName.Equals("Manager")).Id;
            ticket.DepartureStationId = depSta.Id;
            ticket.DestinationStationId = desSta.Id;
            ticket.State = false;
            ticket.Price = 0;
        }
    }
}