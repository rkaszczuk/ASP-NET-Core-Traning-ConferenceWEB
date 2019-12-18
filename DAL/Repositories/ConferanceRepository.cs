using Shared.Models;
using Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ConferanceRepository : IConferanceRepository
    {
        private ConferanceDbContext dbctx;
        public ConferanceRepository(ConferanceDbContext dbContext)
        {
            dbctx = dbContext;
        }
        public Conference AddOrUpdate(Conference model)
        {
            if (model.Id <= 0)
            {
                dbctx.Conferences.Add(model);
            }
            else 
            {
                dbctx.Attach(model);
                dbctx.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            dbctx.SaveChanges();
            return model;
        }

        public IEnumerable<Conference> GetAll()
        {
            return dbctx.Conferences.ToList();
        }

        public Conference GetById(int id)
        {
            return dbctx.Conferences.FirstOrDefault(x => x.Id == id);
        }
    }
}
