namespace TripShare.Models
{
    using System.Collections.Generic;

    public class City
    {

        private ICollection<Trip> trips;
        private ICollection<User> users;

        public City()
        {
            this.trips = new HashSet<Trip>();
            this.users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}