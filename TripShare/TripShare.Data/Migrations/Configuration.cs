using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TripShare.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TripShare.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TripShareDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(TripShareDbContext context)
        {
            if (!context.Cities.Any())
            {
                this.SeedCities(context);
            }

            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }

            if (!context.Trips.Any())
            {
                this.SeedTrips(context);
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
            }

            if (!context.PrivateMessages.Any())
            {
                this.SeedPrivateMessages(context);
            }

            if (!context.Notifications.Any())
            {
                this.SeedNotifications(context);
            }
        }

        private void SeedNotifications(TripShareDbContext context)
        {
            var firstUser = context.Users.FirstOrDefault(u => u.UserName.Equals("bigSmoke"));
            var secondUser = context.Users.FirstOrDefault(u => u.UserName.Equals("secondUser"));

            var notification = new Notification()
            {
                Reciever = secondUser,
                RecieverId = secondUser.Id,
                Type = NotificationType.Message
            };
        }

        private void SeedPrivateMessages(TripShareDbContext context)
        {
            var firstUser = context.Users.FirstOrDefault(u => u.UserName.Equals("bigSmoke"));
            var secondUser = context.Users.FirstOrDefault(u => u.UserName.Equals("secondUser"));

            var privateMessage = new PrivateMessage()
            {
                Content = "kam 12 udobno li ti e?",
                Receiver = firstUser,
                ReceiverId = firstUser.Id,
                Sender = secondUser,
                SenderId = secondUser.Id,
                SendDate = DateTime.Now
            };

            context.PrivateMessages.Add(privateMessage);
        }

        private void SeedComments(TripShareDbContext context)
        {
            var author = context.Users.FirstOrDefault(u => u.UserName.Equals("bigSmoke"));
            var trip = context.Trips.FirstOrDefault(t => t.Id == 1);
            var comment = new Comment()
            {
                Author = author,
                AuthorId = author.Id,
                Content = "V kolko patuvash",
                PostedOn = DateTime.Now,
                Trip = trip,
                TripId = trip.Id
            };

            context.Comments.Add(comment);
            context.SaveChanges();
        }

        private void SeedTrips(TripShareDbContext context)
        {
            var driver = context.Users.FirstOrDefault(u => u.UserName.Equals("secondUser"));
            var departureCity = context.Cities.FirstOrDefault(c => c.Name.Equals("Turnovo"));
            var arrivalCity = context.Cities.FirstOrDefault(c => c.Name.Equals("Ruse"));

            var trip = new Trip()
            {
                Title = "Turnovo-Ruse",
                DriverId = driver.Id,
                Driver = driver,
                AvailableSeats = 3,
                DepartureCityId = departureCity.Id,
                DepartureCity = departureCity,
                ArrivalCityId = arrivalCity.Id,
                ArrivalCity = arrivalCity
            };

            var secondTrip = new Trip()
            {
                Title = "Ruse-Turnovo",
                DriverId = driver.Id,
                Driver = driver,
                AvailableSeats = 2,
                DepartureCityId = arrivalCity.Id,
                DepartureCity = arrivalCity,
                ArrivalCityId = departureCity.Id,
                ArrivalCity = departureCity
            };

            context.Trips.Add(trip);
            context.Trips.Add(secondTrip);

            context.SaveChanges();
        }

        private void SeedUsers(TripShareDbContext context)
        {
            var user = new User()
            {
                UserName = "bigSmoke",
                Email = "richard@gmail.bg"
            };

            var secondUser = new User()
            {
                UserName = "secondUser",
                Email = "pesho@gmail.com"
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            const string Password = "bigSm0ke!";
            const string SecondPass = "web1api!";

            var userCreateResult = userManager.Create(user, Password);
            var secondUserCreateResult = userManager.Create(secondUser, SecondPass);

            if (!userCreateResult.Succeeded || !secondUserCreateResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(
                    Environment.NewLine,
                    userCreateResult.Errors));
            }
        }

        private void SeedCities(TripShareDbContext context)
        {
            context.Cities.Add(new City()
            {
                Name = "Turnovo"
            });

            context.Cities.Add(new City()
            {
                Name = "Mihaylovgrad"
            });

            context.Cities.Add(new City()
            {
                Name = "Ruse"
            });

            context.Cities.Add(new City()
            {
                Name = "Varna"
            });

            context.Cities.Add(new City()
            {
                Name = "Plovdiv"
            });

            context.Cities.Add(new City()
            {
                Name = "Sofiq"
            });


            context.Cities.Add(new City()
            {
                Name = "Burgas"
            });

            context.SaveChanges();
        }
    }
}
