using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DpmWebsite.Controllers
{
    [Route("api")]
    public class SampleDataController : Controller
    {
        public SampleDataController(DpmService service, DpmGraphQLService dpmGraphQL)
        {
            _service = service;
            _dpmGraphQL = dpmGraphQL;
        }

        [HttpGet("DbQuery")]
        public IReadOnlyList<DbPlugin> DbQuery()
        {
            return _service.ShowPlugins();
        }

        [HttpGet("graphQL")]
        public object GraphQL()
        {
            return _dpmGraphQL.GetHello();
        }

        [HttpGet("log")]
        public object Visitors()
        {
            _service.RecordVisit(Request.Headers["X-Forwarded-For"], Request.Headers["User-Agent"]);
            return new { visitors = _service.GetVisitors() };
        }

        private readonly DpmService _service;
        private readonly DpmGraphQLService _dpmGraphQL;
    }
}
