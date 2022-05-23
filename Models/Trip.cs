using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Trip
    {
        public int Id { get; set; }

        public Route Route { get; set; }

        public DateTime DepartureDate { get; set; }

        public int MovingTime { get; set; }
    }
}