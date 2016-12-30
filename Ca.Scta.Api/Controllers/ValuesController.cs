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
        public async Task<IHttpActionResult> GetValues(int t)
        {
            await Task.Delay(1000*t);

            var value = $"It has been {t} seconds.";
            return Ok(value);
        }
    }
}
