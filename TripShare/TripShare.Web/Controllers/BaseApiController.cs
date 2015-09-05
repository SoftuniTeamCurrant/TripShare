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
            :this(new TripShareData())
        {            
        }

        public BaseApiController(ITripShareData data)
        {
            this.Data = data;
        }

        public ITripShareData Data { get; private set; }
    }
}
