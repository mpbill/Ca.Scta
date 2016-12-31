using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ca.Scta.Api.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("{t}")]
        public IHttpActionResult GetValues(int t)
        {
            

            var value = $"{t}";
            return Ok(value);
        }
        [HttpGet]
        [Authorize]
        [Route("authenticated/{t}")]
        public IHttpActionResult GetAuthenticatedValuesValues(int t)
        {


            var value = $"authenticated {t}";
            return Ok(value);
        }
    }
}
