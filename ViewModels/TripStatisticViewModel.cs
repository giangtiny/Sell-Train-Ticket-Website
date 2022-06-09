using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.ViewModels
{
    public class TripStatisticViewModel
    {
        public IEnumerable<TripStatistic> TripStatistics { get; set; }
        public long RevenueSunday { get; set; }
        public long RevenueMonday { get; set; }
        public long RevenueTuesday { get; set; }
        public long RevenueWednesday { get; set; }
        public long RevenueThursday { get; set; }
        public long RevenueFriday { get; set; }
        public long RevenueSaturday { get; set; }
    }
}