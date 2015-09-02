namespace TripShare.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using TripShare.Data.Repositories;
    using TripShare.Models;

    public class TripShareData : ITripShareData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public TripShareData() 
            : this(new TripShareDbContext())
        {
        }

        public TripShareData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Trip> Trips 
        {
            get
            {
                return this.GetRepository<Trip>();
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return this.GetRepository<City>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Notification> Notifications
        {
            get
            {
                return this.GetRepository<Notification>();
            }
        }

        public IRepository<PrivateMessage> PrivateMessages
        {
            get
            {
                return this.GetRepository<PrivateMessage>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>()
            where T : class
        {
            var typeOfRepository = typeof(T);

            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
