using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConferanceWeb
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Body { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public void OnGet()
        {
            Title = "Tutaj wpisz tytuł";
            Body = "Tutaj treśc wiadomości";
            Email = "Tutaj adres email";
        }
        public void OnPost()
        {

        }
    }
}