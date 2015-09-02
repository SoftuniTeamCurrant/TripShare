namespace TripShare.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {

        private ICollection<Trip> ownTrips;
        private ICollection<Trip> joinedTrips;
        private ICollection<Comment> comments;

        public User()
        {
            this.ownTrips = new HashSet<Trip>();
            this.joinedTrips = new HashSet<Trip>();
            this.comments = new HashSet<Comment>();
        }

        public bool isDriver { get; set; }

        public virtual ICollection<Trip> OwnTrips
        {
            get { return this.ownTrips; }
            set { this.ownTrips = value; }
        }

        public virtual ICollection<Trip> JoinedTrips
        {
            get { return this.joinedTrips; }
            set { this.joinedTrips = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
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
