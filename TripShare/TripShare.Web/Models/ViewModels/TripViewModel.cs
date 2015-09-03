using System;
using System.Linq.Expressions;
using Microsoft.Ajax.Utilities;
using TripShare.Models;

namespace TripShare.Web.Models.ViewModels
{
    public class TripViewModel
    {
        public string Title { get; set; }

        public string DriverName { get; set; }

        public string Description { get; set; }

        public int AvailableSeats { get; set; }

        public DateTime? DepartureTime { get; set; }

        public string DepartureCityName { get; set; }

        public string ArrivalCityName { get; set; }

        public static Expression<Func<Trip, TripViewModel>> Create
        {
            get
            {
                return p => new TripViewModel()
                {
                    ArrivalCityName = p.ArrivalCity.Name,
                    Description = p.Description,
                    Title = p.Title,
                    AvailableSeats = p.AvailableSeats,
                    DriverName = p.Driver.UserName,
                    DepartureCityName = p.DepartureCity.Name,
                    DepartureTime = p.DepartureDate
                };
            }
        }
    }
}