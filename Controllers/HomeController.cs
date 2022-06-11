using Sell_Train_Ticket.Models;
using Sell_Train_Ticket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("Index", "Customer");
            }

            var viewModel = new FindTripViewModel
            {
                DepartureDate = new DateTime(),
                Stations = _context.Stations.ToList()
            };

            return View(viewModel);
        }

        public ActionResult Faqs()
        {
            return View();
        }
    }
}