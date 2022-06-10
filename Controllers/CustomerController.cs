using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sell_Train_Ticket.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var customers = _context.Users.Where(u => !u.FullName.Contains("Manager")).ToList();

            return View(customers);
        }

        public ActionResult Edit(string id)
        {
            var customer = _context.Users.SingleOrDefault(u => u.Id.Equals(id));
            if (customer == null)
                return HttpNotFound();

            return View("Edit", customer);
        }

        public ActionResult EditCustomer(ApplicationUser customer)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", "Customer");
            }

            var customerInDb = _context.Users.SingleOrDefault(u => u.Id.Equals(customer.Id));
            customerInDb.FullName = customer.FullName;
            customerInDb.Email = customer.Email;
            customerInDb.UserName = customer.Email;
            customerInDb.PhoneNumber = customer.PhoneNumber;
            customerInDb.DateOfBirth = customer.DateOfBirth;

            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Delete(string id)
        {
            var customerInDb = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

            if (customerInDb == null)
                return HttpNotFound();

            _context.Users.Remove(customerInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

    }
}