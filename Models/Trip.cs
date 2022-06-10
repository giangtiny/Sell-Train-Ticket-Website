using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Route")]
        [ForeignKey("Route")]
        public int RouteId { get; set; }

        public Route Route { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Display(Name = "Train")]
        [ForeignKey("Train")]
        public int TrainId { get; set; }

        public Train Train { get; set; }

        public bool IsReverse { get; set; }
    }
}