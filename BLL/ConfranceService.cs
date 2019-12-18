using Shared.Models;
using Shared.Repositories;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class ConfranceService : IConferanceService
    {
        private IConferanceRepository conferanceRepository;
        public ConfranceService(IConferanceRepository conferanceRepository)
        {
            this.conferanceRepository = conferanceRepository;
        }

        public int Add(Conference model)
        {
            if (String.IsNullOrEmpty(model.Name))
            {
                throw new Exception("Konferencja musi mieć nazwę");
            }
            if(model.Start < DateTime.Now)
            {
                throw new Exception("Konferencja nie może mieć daty przeszłej");
            }
            var newConference = conferanceRepository.AddOrUpdate(model);
            return newConference.Id;
        }

        public int AddProposal(int conferanceId, Proposal proposal)
        {
            var conferance = conferanceRepository.GetById(conferanceId);
            conferance.Proposals.Add(proposal);
            conferanceRepository.AddOrUpdate(conferance);
            return proposal.Id;
        }

        public IEnumerable<Conference> GetAll()
        {
            return conferanceRepository.GetAll();
        }

        public Conference GetById(int id)
        {
            return conferanceRepository.GetById(id);
        }
    }
}
