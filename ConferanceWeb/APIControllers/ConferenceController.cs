using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Services;

namespace ConferanceWeb.APIControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private IConferanceService conferanceService;
        public ConferenceController(IConferanceService conferanceService)
        {
            this.conferanceService = conferanceService;
        }

        [FormatFilter]
        [HttpGet("{format?}")]
        //[Produces("application/xml")]
        public ActionResult<IEnumerable<Conference>> GetAll()
        {
            return Ok(this.conferanceService.GetAll());
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