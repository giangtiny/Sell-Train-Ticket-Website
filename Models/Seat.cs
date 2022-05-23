using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public Wagon Wagon { get; set; }

        public SeatType SeatType { get; set; }
    }
}