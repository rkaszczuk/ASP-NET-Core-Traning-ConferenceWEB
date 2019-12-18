using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConferanceWeb.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.Models;
using Shared.Services;

namespace ConferanceWeb.APIControllers
{
    [FormatFilter]
    [Route("api/[controller]/[action]/{format?}")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private IConferanceService conferanceService;
        private IMemoryCache memoryCache;
        public ConferenceController(IConferanceService conferanceService, IMemoryCache memoryCache)
        {
            this.conferanceService = conferanceService;
            this.memoryCache = memoryCache;
        }

        [ResponseCache(CacheProfileName = "5min")]
        //[ResponseCache(Duration = 300)]
        //[FormatFilter]
        //[HttpGet("{format=xml}")]
        //[Produces("application/xml")]
        [HttpGet]
        [ExecutionTimeFilter]
        public ActionResult<IEnumerable<Conference>> GetAll()
        {

            var result = memoryCache.GetOrCreate("CONFERANCE_ALL", (x)=> {
                x.SlidingExpiration = TimeSpan.FromSeconds(30);
                return this.conferanceService.GetAll();
                });

            return Ok(result);
        }

        [HttpGet()]

        public ActionResult<Conference> Get(int id)
        {
            return Ok(this.conferanceService.GetById(id));
        }
        [HttpPost]
        public ActionResult Add(Conference conference)
        {
            var result = this.conferanceService.Add(conference);
            if(result > 0)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}