using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    [Authorize(Roles = "Manager")]
    public class RouteController : Controller
    {
        private ApplicationDbContext _context;

        public RouteController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Route 
        public ActionResult Index()
        {
            var routes = _context.Routes.ToList();

            return View(routes);
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
            var facade = new Facade(_context);
            facade.DeleteRoute(id);

            return RedirectToAction("Index", "Route");
        }
    }
}