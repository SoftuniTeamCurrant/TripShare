using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripShare.Data;

namespace TripShare.Web.Controllers
{
    
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        [Route("api/cities")]
        public IHttpActionResult GetCities()
        {
            var citiesList = this.Data.Cities.All().OrderBy(c => c.Name);

            return this.Ok(citiesList);

        }
    }
}
