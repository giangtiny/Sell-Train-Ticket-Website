using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class TimeBetweenStation
    {
        public int Id { get; set; }

        public Station FirstStation { get; set; }

        public Station SecondStation { get; set; }

        public int MovingTime { get; set; }
    }
}