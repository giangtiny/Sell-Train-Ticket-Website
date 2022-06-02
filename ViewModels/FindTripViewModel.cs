﻿using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.ViewModels
{
    public class FindTripViewModel
    {
        public Station DepartureStation { get; set; }
        public Station DestinationStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public IEnumerable<Station> Stations { get; set; }
    }
}