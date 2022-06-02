using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Wagon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "Train")]
        [ForeignKey("Train")]
        public int TrainId { get; set; }

        public Train Train { get; set; }
    }
}