using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public ApplicationUser Customer { get; set; }

        [Required]
        [ForeignKey("Trip")]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        [Display(Name = "Departure Station")]
        [ForeignKey("DepartureStation")]
        public int DepartureStationId { get; set; }

        public Station DepartureStation { get; set; }

        [Required]
        [Display(Name = "Destination Station")]
        [ForeignKey("DestinationStation")]
        public int DestinationStationId { get; set; }

        public Station DestinationStation { get; set; }

        [Required]
        [Display(Name = "Seat")]
        [ForeignKey("Seat")]
        public int SeatId { get; set; }

        public Seat Seat { get; set; }

        public bool State { get; set; }

        public int Price { get; set; }
    }
}