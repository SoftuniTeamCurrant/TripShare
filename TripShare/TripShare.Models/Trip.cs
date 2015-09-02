namespace TripShare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trip
    {
        private ICollection<User> joinedPeople; 

        private ICollection<Comment> comments;

        public Trip()
        {
            this.comments = new HashSet<Comment>();
            this.joinedPeople = new HashSet<User>();
        }

        public int Id { get; set; }

        public string TripOwnerId { get; set; }

        public virtual User TripOwner { get; set; }

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
        public virtual City DepartureCity { get; set; }

        public int ArrivalCityId { get; set; }
        [ForeignKey("ArrivalCityId")]
        public virtual City ArrivalCity { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<User> JoinedPeople
        {
            get { return this.joinedPeople; }
            set { this.joinedPeople = value; }
        }
    }
}