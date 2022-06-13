using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    //Apply Prototype design pattern
    public class Ticket : ICloneable
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

        public Ticket()
        {

        }

        public Ticket(string _customerId, int _tripId, int _departureStationId, int _destinationStationId, int _seatId, bool _state, int _price)
        {
            CustomerId = _customerId;
            TripId = _tripId;
            DepartureStationId = _departureStationId;
            DestinationStationId = _destinationStationId;
            SeatId = _seatId;
            State = _state;
            Price = _price;
        }

        public object Clone()
        {
            Ticket ticket = new Ticket(CustomerId, TripId, DepartureStationId, DestinationStationId, SeatId, State, Price);

            return ticket;
        }

        public void Delete(ApplicationDbContext context)
        {
            context.Tickets.Remove(this);
        }
    }
}