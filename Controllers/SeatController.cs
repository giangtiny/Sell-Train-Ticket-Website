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
    public class SeatController : Controller
    {
        private ApplicationDbContext _context;

        public SeatController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var seats = _context.Seats.Include("Wagon").Include("SeatType").ToList();

            return View(seats);
        }

        public ActionResult Add()
        {
            var viewModel = new SeatViewModel
            {
                Seat = new Seat(),
                Wagons = _context.Wagons.ToList(),
                SeatTypes = _context.SeatTypes.ToList()
            };

            return View("Save", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var seat = _context.Seats.SingleOrDefault(s => s.Id == id);
            if (seat == null)
                return HttpNotFound();

            var viewModel = new SeatViewModel
            {
                Seat = seat,
                Wagons = _context.Wagons.ToList(),
                SeatTypes = _context.SeatTypes.ToList()
            };

            return View("Save", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Seat seat)
        {
            if (!ModelState.IsValid) 
            {
                var viewModel = new SeatViewModel
                {
                    Seat = new Seat(),
                    Wagons = _context.Wagons.ToList(),
                    SeatTypes = _context.SeatTypes.ToList()
                };

                return View(viewModel); 
            }
            if (seat.Id == 0) 
            {
                _context.Seats.Add(seat); 
            }
            else
            {
                var seatInDb = _context.Seats.Single(s => s.Id == seat.Id);

                seatInDb.Name = seat.Name;
                seatInDb.SeatTypeId = seat.SeatTypeId;
                seatInDb.WagonId = seat.WagonId; 
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Seat"); 
        } 

        public ActionResult Delete(int id)
        {
            var seatInDb = _context.Seats.SingleOrDefault(s => s.Id == id);
            if (seatInDb == null)
            {
                return HttpNotFound();
            }
            //Check whether the deleted seat is in relation with any ticket
            var tickets = _context.Tickets.Where(t => t.SeatId == seatInDb.Id).ToList(); 
            //If so, return to seat index page and annouce
            if (tickets.Count() > 0) 
            {
                var seats = _context.Seats.Include("Wagon").Include("SeatType").ToList();
                ViewBag.Message = ("Can't delete this seat!");

                return View("/Views/Seat/Index.cshtml", seats); 
            }

            _context.Seats.Remove(seatInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Seat"); 
        }
    }
}