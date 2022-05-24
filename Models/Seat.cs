using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WagonId { get; set; }

        public int SeatTypeId { get; set; }
    }
}