using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripShare.Data;
using TripShare.Models;
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
            var trips = this.Data.Trips.All().Select(TripViewModel.Create);

            return this.Ok(trips);
        }
    }
}
