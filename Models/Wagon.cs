using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Wagon
    {
        public int Id { get; set; }

        public Train Train { get; set; }
    }
}