using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class AircraftRepository : IRepository<Aircraft>
    {
        protected readonly DataContext context;

        public AircraftRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Aircraft> Create(Aircraft entity)
        {
            await context.Aicrafts.AddAsync(entity); 
            return entity;
        }

        public async Task Delete(int id)
        {
            Aircraft aircraft = await GetById(id);
            if (aircraft == null)
                throw new System.Exception("Incorrect id");
            //foreach (var departure in aircraft.Departures)
            //{
            //    departure.Flight.Departures.Remove(departure);
            //    context.Departures.Remove(departure);
            //}
            //context.AircraftTypes.Remove(aircraft.AircraftType);
            context.Aicrafts.Remove(aircraft);
        }

        public async Task<IEnumerable<Aircraft>> GetAll()
        {
            return await context.Aicrafts.ToListAsync();
        }

        public async Task<Aircraft> GetById(int id)
        {
            return await context.Aicrafts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Aircraft> Update(int id,Aircraft entity)
        {
            Aircraft aircraft = await GetById(id);
            if (aircraft == null)
                throw new System.Exception("Incorrect id");
            aircraft.IdAircraftType = entity.IdAircraftType;
            aircraft.AircraftType = context.AircraftTypes.FirstOrDefault(at=>at.Id==entity.IdAircraftType);
            aircraft.LifeSpan = entity.LifeSpan;
            aircraft.ReleaseDate = entity.ReleaseDate;
            aircraft.Name = entity.Name;
            return aircraft;
        }
    }
}
