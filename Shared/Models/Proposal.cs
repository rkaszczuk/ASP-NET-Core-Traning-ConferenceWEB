using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
        public string Speaker { get; set; }
        public string Title { get; set; }
        public bool Approved { get; set; }
    }
}
