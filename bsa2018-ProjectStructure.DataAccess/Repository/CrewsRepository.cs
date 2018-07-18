using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class CrewsRepository : IRepository<Crew>
    {
        protected readonly DataContext context;

        public CrewsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Crew> Create(Crew entity)
        {
            await context.Crews.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            Crew crew = await GetById(id);
            if (crew == null)
                throw new System.Exception("Incorrect id");
            context.Crews.Remove(crew);
        }

        public async Task<IEnumerable<Crew>> GetAll()
        {
            return await context.Crews.ToListAsync();
        }

        public async Task<Crew> GetById(int id)
        {
            return await context.Crews.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Crew> Update(int id, Crew entity)
        {
            Crew crew = await GetById(id);
            if (crew == null)
                throw new System.Exception("Incorrect id");
            crew.IdPilot = entity.IdPilot;
            crew.Pilot = context.Pilots.FirstOrDefault(p => p.Id == entity.IdPilot);
            crew.StewardessCrews = entity.StewardessCrews;
            return crew;
        }
    }
}
