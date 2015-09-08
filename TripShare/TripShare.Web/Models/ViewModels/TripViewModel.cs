using System.Linq;

namespace TripShare.Web.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using Microsoft.Ajax.Utilities;
    using TripShare.Models;
    public class TripViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DriverName { get; set; }

        public string Description { get; set; }

        public int AvailableSeats { get; set; }

        public DateTime? DepartureTime { get; set; }

        public string DepartureCityName { get; set; }

        public string ArrivalCityName { get; set; }

        public ICollection<UserViewModel> Passengers { get; set; }

        public int CommentsCount { get; set; }

        public static Expression<Func<Trip, TripViewModel>> Create
        {
            get
            {
                return p => new TripViewModel()
                {
                    Id = p.Id,
                    ArrivalCityName = p.ArrivalCity.Name,
                    Description = p.Description,
                    Title = p.Title,
                    AvailableSeats = p.AvailableSeats,
                    DriverName = p.Driver.UserName,
                    DepartureCityName = p.DepartureCity.Name,
                    DepartureTime = p.DepartureDate,
                    Passengers = p.Passengers.AsQueryable().Select(UserViewModel.Create).ToList(),
                    CommentsCount = p.Comments.Count
                };
            }
        }
    }
}