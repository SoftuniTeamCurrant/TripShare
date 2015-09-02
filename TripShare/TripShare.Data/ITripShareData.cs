namespace TripShare.Data
{
    using TripShare.Data.Repositories;
    using TripShare.Models;


    public interface ITripShareData
    {
        IRepository<User> Users { get; }

        IRepository<Trip> Trips { get; }

        int SaveChanges();
    }
}
