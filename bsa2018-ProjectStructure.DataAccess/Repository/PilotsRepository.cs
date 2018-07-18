using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class PilotsRepository : IRepository<Pilot>
    {
        protected readonly DataContext context;

        public PilotsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Pilot> Create(Pilot entity)
        {
            await context.Pilots.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            Pilot pilot = await GetById(id);
            if (pilot == null)
                throw new System.Exception("Incorrect id");
            context.Pilots.Remove(pilot);
        }

        public async Task<IEnumerable<Pilot>> GetAll()
        {
            return await context.Pilots.ToListAsync();
        }

        public async Task<Pilot> GetById(int id)
        {
            return await context.Pilots.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Pilot> Update(int id, Pilot entity)
        {
            Pilot pilot = await GetById(id);
            if (pilot == null)
                throw new System.Exception("Incorrect id");
            pilot.Birthday = entity.Birthday;
            pilot.Experience = entity.Experience;
            pilot.Name = entity.Name;
            pilot.Surname = entity.Surname;
            return pilot;
        }
    }
}
