using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DpmWebsite.Controllers
{
    [Route("api")]
    public class SampleDataController : Controller
    {
        public SampleDataController(DpmService service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public IReadOnlyList<DbPlugin> DbQuery()
        {
            return _service.ShowPlugins();
        }

        [HttpGet("log")]
        public object Visitors()
        {
            _service.RecordVisit(HttpContext.Connection.RemoteIpAddress.ToString(), Request.Headers["User-Agent"]);
            return new { visitors = _service.GetVisitors() };
        }

        private readonly DpmService _service;
    }
}
