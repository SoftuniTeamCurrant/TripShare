using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using TripShare.Data;
using TripShare.Models;
using TripShare.Web.Models.BindingModels;
using TripShare.Web.Models.ViewModels;

namespace TripShare.Web.Controllers
{
    [Authorize]
    public class TripsController : ApiController
    {
        public TripsController(ITripShareData data)
        {
            this.Data = data;
        }

        public ITripShareData Data { get; private set; }

        public IHttpActionResult GetTrips()
        {
            var trips = this.Data.Trips.All().Select(TripViewModel.Create); //One Query

            return this.Ok(trips);
        }

        public IHttpActionResult PostTrip(AddTripBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null (no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var departureCity = this.Data.Cities.Find(model.DepartureCityId);
            var arrivalCity = this.Data.Cities.Find(model.ArrivalCityId);

            if (departureCity == null)
            {
                return this.BadRequest("Departure city does not exsist!");
            }

            if (arrivalCity == null)
            {
                return this.BadRequest("Arrival city does not exsist!");
            }

            var trip = new Trip()
            {
                Title = model.Title,
                ArrivalCityId = model.ArrivalCityId,
                Description = model.Description,
                AvailableSeats = model.AvaibleSeats,
                DepartureCityId = model.DepartureCityId,
                DepartureDate = model.DepartureDate,
                DriverId = this.User.Identity.GetUserId()
            };

            this.Data.Trips.Add(trip);
            this.Data.SaveChanges();

            var data = this.Data.Trips
                .All().Where(t => t.Id == trip.Id)
                .Select(TripViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);

        }
    }
}
