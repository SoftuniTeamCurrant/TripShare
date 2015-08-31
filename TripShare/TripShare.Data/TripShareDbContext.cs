namespace TripShare.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using TripShare.Models;

    public class TripShareDbContext : IdentityDbContext<User>
    {
        public TripShareDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TripShareDbContext Create()
        {
            return new TripShareDbContext();
        }
    }
}
