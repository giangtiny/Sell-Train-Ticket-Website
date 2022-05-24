using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sell_Train_Ticket.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TimeBetweenStation> TimeBetweenStations { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Wagon> Wagons { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}