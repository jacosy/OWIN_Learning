using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OwinDemo.Controllers
{
    [RoutePrefix("api")]
    public class HelloWorldApiController : ApiController
    {
        [HttpGet, Route("Hello")]
        public IHttpActionResult HelloWorld()
        {
            return Content<string>(System.Net.HttpStatusCode.OK, "Hello from Hello World Web Api");
        }
    }
}