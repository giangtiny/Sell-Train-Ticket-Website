using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Wagon")]
        [ForeignKey("Wagon")]
        public int WagonId { get; set; }

        public Wagon Wagon { get; set; }

        [Required]
        [Display(Name = "Seat Type")]
        [ForeignKey("SeatType")]
        public int SeatTypeId { get; set; }

        public SeatType SeatType { get; set; }
    }
}