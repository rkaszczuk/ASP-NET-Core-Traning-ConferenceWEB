using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferanceWeb.APIControllers.V2
{
    //api/v1.0/Home/Version
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Version()
        {
            var lastUsedVersion = Request.Cookies["LastUsedVersion"];
            Response.Cookies.Append("LastUsedVersion", "2.0-BETA");
            return new ObjectResult(new { Version = "2.0-BETA", LastUsedVersion = lastUsedVersion });
        }
    }
}