using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Station
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParkingTime { get; set; }

        public Route Route { get; set; }
    }
}