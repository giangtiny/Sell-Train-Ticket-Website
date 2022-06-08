using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class TripStatistic
    {
        public int Id { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        public long Revenue { get; set; }

        public int TotalTicket { get; set; }

        public int TicketInStock { get; set; }
    }
}