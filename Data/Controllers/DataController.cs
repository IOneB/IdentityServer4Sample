using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DataController : ControllerBase
    {
        public DataController(ILogger<DataController> logger)
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value});
        }
    }
}
