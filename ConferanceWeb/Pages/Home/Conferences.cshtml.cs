using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Models;
using Shared.Services;

namespace ConferanceWeb
{
    public class ConferencesModel : PageModel
    {
        private IConferanceService conferanceService;
        public IEnumerable<Conference> Conferences;
        public ConferencesModel(IConferanceService conferanceService)
        {
            this.conferanceService = conferanceService;
        }
        public void OnGet()
        {
            Conferences = this.conferanceService.GetAll();
        }
    }
}