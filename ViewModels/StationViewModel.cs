using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.ViewModels
{
    public class StationViewModel
    {
        public Station Station { get; set; }
        public IEnumerable<Route> Routes { get; set; }
    }
}
