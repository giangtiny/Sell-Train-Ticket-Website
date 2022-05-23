using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public ApplicationUser Customer { get; set; }

        public Trip Trip { get; set; }

        public Station DepartureStation { get; set; }

        public Station DestinationStation { get; set; }

        public Seat Seat { get; set; }

        public bool IsKid { get; set; }

        public bool State { get; set; }
    }
}