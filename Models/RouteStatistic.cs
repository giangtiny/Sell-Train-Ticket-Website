using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class RouteStatistic
    {
        public Route Route { get; set; }
        public long Revenue { get; set; }
        public int TotalTicket { get; set; }
        public int TicketInStock { get; set; }
    }
}