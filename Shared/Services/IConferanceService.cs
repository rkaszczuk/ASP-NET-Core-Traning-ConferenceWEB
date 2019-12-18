using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IConferanceService
    {
        IEnumerable<Conference> GetAll();
        Conference GetById(int id);
        int Add(Conference model);
        int AddProposal(int conferanceId, Proposal proposal);
    }
}
