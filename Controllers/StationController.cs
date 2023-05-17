using Sell_Train_Ticket.Models;
using Sell_Train_Ticket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class StationController : Controller
    {
        private ApplicationDbContext _context;

        public StationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Station 
        public ActionResult Index()
        {
            var stations = _context.Stations.Include("Route").ToList();

            return View(stations);
        }

        public ActionResult Add()
        {
            var viewModel = new StationViewModel
            {
                Station = new Station(),
                Routes = _context.Routes.ToList()
            };

            return View("Save", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var station = _context.Stations.SingleOrDefault(s => s.Id == id);
            if (station == null)
                return HttpNotFound();

            var viewModel = new StationViewModel
            {
                Station = station,
                Routes = _context.Routes.ToList()
            };

            return View("Save", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Station station)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new StationViewModel
                {
                    Station = new Station(),
                    Routes = _context.Routes.ToList()
                };

                return View(viewModel);
            }
            if (station.Id == 0)
            {
                _context.Stations.Add(station);
            }
            else
            {
                var stationInDb = _context.Stations.Single(s => s.Id == station.Id);

                stationInDb.Name = station.Name;
                stationInDb.ParkingTime = station.ParkingTime;
                stationInDb.RouteId = station.RouteId;
                stationInDb.IsFirst = station.IsFirst;
                stationInDb.IsFinal = station.IsFinal;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Station");
        }

        public ActionResult Delete(int id)
        {
            var stationInDb = _context.Stations.SingleOrDefault(s => s.Id == id);
            if (stationInDb == null)
            {
                return HttpNotFound();
            }

            //Check whether the deleted station is in relation with any ticket
            var tickets = _context.Tickets
                .Where(t => t.DepartureStationId == stationInDb.Id
                || t.DestinationStationId == stationInDb.Id).ToList();
            //If so, return to station index page and annouce
            if(tickets.Count() > 0)
            {
                var stations = _context.Stations.Include("Route").ToList();
                ViewBag.Message = ("Can't delete this station!");

                return View("/Views/Station/Index.cshtml", stations);
            }

            _context.Stations.Remove(stationInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Station");
        }
    }
}