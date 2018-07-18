using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class StewardessRepository : IRepository<Stewardess>
    {
        protected readonly DataContext context;

        public StewardessRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Stewardess> Create(Stewardess entity)
        {
            await context.Stewardess.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            Stewardess stewardess = await GetById(id);
            if (stewardess == null)
                throw new System.Exception("Incorrect id");
            context.Stewardess.Remove(stewardess);
        }

        public async Task<IEnumerable<Stewardess>> GetAll()
        {
            return await context.Stewardess.ToListAsync();
        }

        public async Task<Stewardess> GetById(int id)
        {
            return await context.Stewardess.FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<Stewardess> Update(int id, Stewardess entity)
        {
            Stewardess stewardess = await GetById(id);
            if (stewardess == null)
                throw new System.Exception("Incorrect id");
            stewardess.Birthday = entity.Birthday;
            stewardess.Name = entity.Name;
            stewardess.Surname = entity.Surname;
            return stewardess;
        }
    }
}
