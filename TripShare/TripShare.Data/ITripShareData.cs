namespace TripShare.Data
{
    using TripShare.Data.Repositories;
    using TripShare.Models;


    public interface ITripShareData
    {
        IRepository<User> Users { get; }

        IRepository<Trip> Trips { get; }

        IRepository<City> Cities { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<PrivateMessage> PrivateMessages { get; }

        int SaveChanges();
    }
}
