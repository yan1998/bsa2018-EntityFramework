using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class DeparturesRepository : IRepository<Departure>
    {
        protected readonly DataContext context;

        public DeparturesRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Departure> Create(Departure entity)
        {
            await context.Departures.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            Departure departure = await GetById(id);
            if (departure == null)
                throw new System.Exception("Incorrect id");
            context.Departures.Remove(departure);
        }

        public async Task<IEnumerable<Departure>> GetAll()
        {
            return await context.Departures.ToListAsync();
        }

        public async Task<Departure> GetById(int id)
        {
            return await context.Departures.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Departure> Update(int id, Departure entity)
        {
            Departure departure = await GetById(id);
            if (departure == null)
                throw new System.Exception("Incorrect id");
            departure.DepartureTime = entity.DepartureTime;
            departure.IdAircraft = entity.IdAircraft;
            departure.Aircraft = context.Aicrafts.FirstOrDefault(a => a.Id == entity.IdAircraft);
            departure.IdCrew = entity.IdCrew;
            departure.Crew = context.Crews.FirstOrDefault(c => c.Id == entity.IdCrew);
            departure.IdFlight = entity.IdFlight;
            departure.Flight = context.Flights.FirstOrDefault(f => f.Id == entity.IdFlight);
            return departure;
        }
    }
}
