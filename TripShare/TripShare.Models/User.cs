namespace TripShare.Models
    {
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class User : IdentityUser
        {

        private ICollection<Trip> ownTrips;
        private ICollection<Trip> savedTrips;

        public User()
            {
            this.ownTrips = new HashSet<Trip>();
            this.savedTrips = new HashSet<Trip>();
            }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public int CityId { get; set; }
        public City HomeCity { get; set; }

        public virtual ICollection<Trip> OwnTrip
            {
            get { return this.ownTrips; }
            set { this.ownTrips = value; }
            }
        public virtual ICollection<Trip> SavedTrip
            {
            get { return this.savedTrips; }
            set { this.savedTrips = value; }
            }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
              UserManager<User> manager,
              string authenticationType)
            {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
            }
        }
    }
