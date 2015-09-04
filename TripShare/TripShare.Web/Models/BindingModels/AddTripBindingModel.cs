namespace TripShare.Web.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddTripBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte AvaibleSeats { get; set; }

        public DateTime? DepartureDate { get; set; }

        [Required]
        public int DepartureCityId { get; set; }

        [Required]
        public int ArrivalCityId { get; set; }
    }
}