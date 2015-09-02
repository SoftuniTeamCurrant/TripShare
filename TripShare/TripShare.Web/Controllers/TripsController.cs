using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripShare.Models;

namespace TripShare.Web.Controllers
{
    public class TripsController : BaseApiController
    {
        public IHttpActionResult GetTrips()
        {
            var cities = this.Data.Cities.All().ToList();

            return this.Ok(cities);
        }
    }
}
