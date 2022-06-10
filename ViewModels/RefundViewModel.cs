using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.ViewModels
{
    public class RefundViewModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public string AnnounceMessage { get; set; }
    }
}