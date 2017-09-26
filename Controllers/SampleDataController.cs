using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DpmWebsite.Controllers
{
    [Route("api/[controller]")]
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

        private readonly DpmService _service;
    }
}
