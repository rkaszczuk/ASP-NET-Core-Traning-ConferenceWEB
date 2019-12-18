using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Repositories
{
    public interface IConferanceRepository
    {
        Conference AddOrUpdate(Conference model);
        IEnumerable<Conference> GetAll();
        Conference GetById(int id);
    }
}
