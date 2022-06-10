using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TrainController : Controller
    {
        private ApplicationDbContext _context;

        public TrainController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var trains = _context.Trains.ToList();

            return View(trains);
        }

        public ActionResult Add()
        {
            var train = new Train();

            return View("Save", train);
        }

        public ActionResult Edit(int id)
        {
            var train = _context.Trains.SingleOrDefault(t => t.Id == id);
            if (train == null)
                return HttpNotFound();

            return View("Save", train);
        }

        [HttpPost]
        public ActionResult Save(Train train)
        {
            if (!ModelState.IsValid)
            {
                var newTrain = new Train();

                return View(newTrain);
            }
            if (train.Id == 0)
            {
                _context.Trains.Add(train);
            }
            else
            {
                var trainInDb = _context.Trains.Single(t => t.Id == train.Id);
                trainInDb.Name = train.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Train");
        }

        public ActionResult Delete(int id)
        {
            var trainInDb = _context.Trains.SingleOrDefault(t => t.Id == id);

            if (trainInDb == null)
            {
                return HttpNotFound();
            }

            _context.Trains.Remove(trainInDb);

            _context.SaveChanges();

            return RedirectToAction("Index", "Train");
        }
    }
}