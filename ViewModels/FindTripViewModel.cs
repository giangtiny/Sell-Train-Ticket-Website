using Sell_Train_Ticket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sell_Train_Ticket.ViewModels
{
    public class FindTripViewModel
    {
        [Required]
        [Display(Name = "Departure Station")]
        public int DepartureStation { get; set; }
        [Required]
        [Display(Name = "Destination Station")]
        public int DestinationStation { get; set; }
        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }
        public IEnumerable<Station> Stations { get; set; }
        public string AnnounceMessage { get; set; }
    }
}