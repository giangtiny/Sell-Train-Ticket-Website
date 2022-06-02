using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.ViewModels
{
    public class SeatViewModel
    {
        public Seat Seat { get; set; }
        public IEnumerable<Wagon> Wagons { get; set; }
        public IEnumerable<SeatType> SeatTypes { get; set; }
    }
}