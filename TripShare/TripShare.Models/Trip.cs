namespace TripShare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trip
    {
        private ICollection<Comment> comments;

        public Trip()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public int TripOwnerId { get; set; }

        public User TripOwner { get; set; }

        public string Title { get; set; }

        // [Required]
        // [MinLength(20)]
        public string Description { get; set; }

        //[Required]
        //[MinLength(20)]
        public string AutomobileInformation { get; set; }

        public byte AvailableSeats { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public int DepartureCityId { get; set; }
        [ForeignKey("DepartureCityId")]
        public City DepartureCity { get; set; }

        public int ArrivalCityId { get; set; }
        [ForeignKey("ArrivalCityId")]
        public City ArrivalCity { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}