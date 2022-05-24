using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Route")]
        public int RouteId { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Moving Time")]
        public int MovingTime { get; set; }

        [Required]
        [Display(Name = "Train")]
        public int TrainId { get; set; }
    }
}