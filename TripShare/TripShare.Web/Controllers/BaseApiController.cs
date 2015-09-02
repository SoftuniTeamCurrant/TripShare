using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripShare.Data;

namespace TripShare.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController()
            : this(new TripShareData(new TripShareDbContext()))
        {
            
        }

        public BaseApiController(ITripShareData data)
        {
            this.Data = data;
        }

        protected ITripShareData Data { get; set; }
    }
}
