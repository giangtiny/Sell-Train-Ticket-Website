using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        [Display(Name = "Departure Station")]
        public int DepartureStationId { get; set; }

        [Required]
        [Display(Name = "Destination Station")]
        public int DestinationStationId { get; set; }

        [Required]
        [Display(Name = "Seat")]
        public int SeatId { get; set; }

        [Required]
        [Display(Name = "Ticket for")]
        public bool IsKid { get; set; }

        public bool State { get; set; }
    }
}