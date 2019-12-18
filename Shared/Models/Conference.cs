using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shared.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public string Location { get; set; }
        [DisplayName("Attendee total")]
        public int AttendeeTotal { get; set; }
        public List<Proposal> Proposals { get; set; }
    }
}
