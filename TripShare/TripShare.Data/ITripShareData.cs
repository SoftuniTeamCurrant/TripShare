namespace TripShare.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TripShare.Data.Repositories;
    using TripShare.Models;


    public interface ITripShareData
    {
        IRepository<User> Users { get; }

        IRepository<Trip> Trips { get; }

        int SaveChanges();
    }
}
