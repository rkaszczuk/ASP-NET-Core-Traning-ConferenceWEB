﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferanceWeb.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferanceWeb.APIControllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    //[LogFilter(">>> Uruchomiono sprawdzanie wersji: {0}", ">>> Zakończono sprawdzanie wersji: {0}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Version()
        {
            var lastUsedVersion = Request.Cookies["LastUsedVersion"];
            Response.Cookies.Append("LastUsedVersion", "1.0-BETA");
            return new ObjectResult(new { Version = "1.0-BETA", LastUsedVersion = lastUsedVersion });
        }
    }
}