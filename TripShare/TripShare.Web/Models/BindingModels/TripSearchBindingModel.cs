﻿namespace TripShare.Web.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TripSearchBindingModel
    {           
        public byte? AvaibleSeats { get; set; }

        public DateTime? DepartureDate { get; set; }

        [Required]
        public string DepartureCity { get; set; }

        [Required]
        public string ArrivalCity { get; set; }
    }
}