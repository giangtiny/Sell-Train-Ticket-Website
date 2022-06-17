using Sell_Train_Ticket.Models;
using Sell_Train_Ticket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    [Authorize(Roles = "Manager")]
    public class WagonController : Controller
    {
        private ApplicationDbContext _context;

        public WagonController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var wagons = _context.Wagons.Include("Train").ToList();

            return View(wagons);
        }

        public ActionResult Add()
        {
            var viewModel = new WagonViewModel
            {
                Wagon = new Wagon(),
                Trains = _context.Trains.ToList()
            };

            return View("Save", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var wagon = _context.Wagons.SingleOrDefault(w => w.Id == id);
            if (wagon == null)
                return HttpNotFound();

            var viewModel = new WagonViewModel
            {
                Wagon = wagon,
                Trains = _context.Trains.ToList()
            };

            return View("Save", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Wagon wagon)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new WagonViewModel
                {
                    Wagon = new Wagon(),
                    Trains = _context.Trains.ToList()
                };

                return View(viewModel);
            }
            if (wagon.Id == 0)
            {
                _context.Wagons.Add(wagon);
            }
            else
            {
                var wagonInDb = _context.Wagons.Single(w => w.Id == wagon.Id);

                wagonInDb.Name = wagon.Name;
                wagonInDb.TrainId = wagon.TrainId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Wagon");
        }

        public ActionResult Delete(int id)
        {
            var wagonInDb = _context.Wagons.SingleOrDefault(w => w.Id == id);
            if (wagonInDb == null)
            {
                return HttpNotFound();
            }

            //Check whether the deleted wagon is in relation with any trip
            var trips = _context.Trips.Where(t => t.TrainId == wagonInDb.TrainId).ToList();
            //If so, return to wagon index page and annouce
            if (trips.Count() > 0)
            {
                var wagons = _context.Wagons.Include("Train").ToList();
                ViewBag.Message = ("Can't delete this wagon!");

                return View("/Views/Wagon/Index.cshtml", wagons);
            }

            _context.Wagons.Remove(wagonInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Wagon");
        }
    }
}