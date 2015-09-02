namespace TripShare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trip
    {
        private ICollection<User> passengers;
        private ICollection<Comment> comments;

        public Trip()
        {
            this.comments = new HashSet<Comment>();
            this.passengers = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string DriverId { get; set; }

        public virtual User Driver { get; set; }

        public string Description { get; set; }

        public byte AvailableSeats { get; set; }

        public DateTime? DepartureDate { get; set; }

        public int DepartureCityId { get; set; }

        public virtual City DepartureCity { get; set; }

        public int ArrivalCityId { get; set; }

        public virtual City ArrivalCity { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<User> Passengers
        {
            get { return this.passengers; }
            set { this.passengers = value; }
        }
    }
}