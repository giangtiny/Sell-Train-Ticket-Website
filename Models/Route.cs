using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}