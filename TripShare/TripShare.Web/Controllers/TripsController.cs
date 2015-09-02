using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TripShare.Web.Controllers
{
    public class TripsController : BaseApiController
    {
        public IHttpActionResult GetTrips()
        {
            var trips = this.Data.Trips.All();

            return this.Ok(trips);
        }
    }
}
