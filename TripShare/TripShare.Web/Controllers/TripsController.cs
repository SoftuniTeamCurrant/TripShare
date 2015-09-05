using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
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

        // GET api/trips
        [HttpGet]
        public IHttpActionResult GetTrips()
        {
            var trips = this.Data.Trips.All().Select(TripViewModel.Create); //One Query

            return this.Ok(trips);
        }

        // POST api/trips
        [HttpPost]
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
                return this.BadRequest("Departure city does not exist!");
            }

            if (arrivalCity == null)
            {
                return this.BadRequest("Arrival city does not exist!");
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

        // PUT api/trips/{id}
        [HttpPut]
        public IHttpActionResult UpdateTrip(int id, AddTripBindingModel model)
        {
            var trip = this.Data.Trips.Find(id);

            if (trip == null)
            {
                return this.BadRequest(string.Format("There is no trip with Id {0}", id));
            }

            var loggedUserId = this.User.Identity.GetUserId();

            if (trip.DriverId != loggedUserId)
            {
                return this.Unauthorized();
            }

            if (model == null)
            {
                return this.BadRequest("Model cannot be null (no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            trip.ArrivalCityId = model.ArrivalCityId;
            trip.DepartureCityId = model.DepartureCityId;
            trip.DepartureDate = model.DepartureDate;
            trip.AvailableSeats = model.AvaibleSeats;
            trip.Description = model.Description;
            trip.Title = model.Title;

            this.Data.SaveChanges();

            var data = this.Data.Trips
                .All().Where(t => t.Id == trip.Id)
                .Select(TripViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);

        }

        // DELETE api/trips/{id}
        [HttpDelete]
        public IHttpActionResult DeleteTrip(int id)
        {
            var trip = this.Data.Trips.Find(id);

            if (trip == null)
            {
                return this.BadRequest(string.Format("There is no trip with Id {0}", id));
            }

            var loggedUserId = this.User.Identity.GetUserId();

            if (trip.DriverId != loggedUserId)
            {
                return this.Unauthorized();
            }

            this.Data.Trips.Delete(trip);
            this.Data.SaveChanges();

            return this.Ok();
        }

        // GET api/trips/search
        [HttpGet]
        [Route("api/trips/search")]
        public IHttpActionResult SearchTrip(
            [FromUri]TripSearchBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("TripsSearchBinding model cannot be null. DepartureDate and DepartureCity are mandatory URI parameters");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var tripsSearchReults = this.Data.Trips.All()
                .Where(t => t.DepartureCity.Name == model.DepartureCity && t.ArrivalCity.Name == model.ArrivalCity)
                .Select(TripViewModel.Create);

            //Suggest: Change DeparuteDate to mandatory
            if (model.DepartureDate != null)
            {
                tripsSearchReults = tripsSearchReults
                    .Where(u => DbFunctions.TruncateTime(u.DepartureTime) == DbFunctions.TruncateTime(model.DepartureDate));
            }

            return this.Ok(tripsSearchReults);
        }

        // GET api/trips/myTrips
        [HttpGet]
        [Route("api/trips/my-trips")]
        public IHttpActionResult GetMyTrips()
        {
            var userId = this.User.Identity.GetUserId();
            var trips = this.Data.Trips.All()
                .Where(t => t.DriverId == userId)
                .Select(TripViewModel.Create); //One Query

            return this.Ok(trips);
        }

        // PUT api/trips/{id}/join
        [HttpPut]
        [Route("api/trips/{id}/join")]
        public IHttpActionResult JoinTrip(int id)
        {
            var trip = this.Data.Trips.Find(id);
            if (trip == null)
            {
                return this.BadRequest("No such trip!");
            }

            if (trip.AvailableSeats == 0)
            {
                return this.BadRequest("Trip is full!");
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(loggedUserId);

            if (user.JoinedTrips.Any(t => t.Id == trip.Id))
            {
                return this.BadRequest("You have already joined this trip!");
            }

            if (loggedUserId == trip.DriverId)
            {
                return this.BadRequest("You cannot join your own trip!");
            }

            trip.AvailableSeats = --trip.AvailableSeats;
            user.JoinedTrips.Add(trip);

            var notification = new Notification()
            {
                RecieverId = trip.DriverId,
                Type = NotificationType.Join
            };

            this.Data.Notifications.Add(notification);

            this.Data.SaveChanges();
            return this.Ok();
        }

        // PUT api/trips/{id}/leave
        [HttpPut]
        [Route("api/trips/{id}/leave")]
        public IHttpActionResult LeaveTrip(int id)
        {
            var trip = this.Data.Trips.Find(id);
            var user = this.Data.Users.Find(this.User.Identity.GetUserId());

            if (trip == null)
            {
                return this.BadRequest("No such trip!");
            }

            if (!user.JoinedTrips.Any(t => t.DriverId == trip.DriverId))
            {
                return this.BadRequest("You are not in this trip!");
            }

            trip.AvailableSeats = ++ trip.AvailableSeats;
            user.JoinedTrips.Remove(trip);
            trip.Passengers.Remove(user);

            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}
