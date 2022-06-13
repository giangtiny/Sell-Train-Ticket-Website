using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Station
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Parking Time")]
        [Range(1, int.MaxValue, ErrorMessage = "Parking Time must greater than 0")]
        public int ParkingTime { get; set; }

        [Required]
        [Display(Name = "Route")]
        [ForeignKey("Route")]
        public int RouteId { get; set; }

        public Route Route { get; set; }

        public bool IsFirst { get; set; }

        public bool IsFinal { get; set; }

        public void Delete(ApplicationDbContext context)
        {
            context.Stations.Remove(this);
        }
    }
}