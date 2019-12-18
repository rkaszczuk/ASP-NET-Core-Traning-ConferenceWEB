using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Repositories
{
    public interface IProposalRepository
    {
        Proposal Add(Proposal model);
        Proposal Approve(int proposalId);
        IEnumerable<Proposal> GetAllForConference(int conferenceId);
        Proposal GetById(int id);
    }
}
