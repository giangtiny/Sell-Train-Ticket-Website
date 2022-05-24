using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class TimeBetweenStation
    {
        public int Id { get; set; }

        public int FirstStationId { get; set; }

        public int SecondStationId { get; set; }

        public int MovingTime { get; set; }
    }
}