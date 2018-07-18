using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class FlightsRepository : IRepository<Flight>
    {
        protected readonly DataContext context;

        public FlightsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Flight>> IRepository<Flight>.GetAll()
        {
            return await context.Flights.ToListAsync();
        }

        public async Task<Flight> GetById(int id)
        {
            return await context.Flights.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Flight> Create(Flight entity)
        {
            await context.Flights.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            Flight flight = await GetById(id);
            if (flight == null)
                throw new System.Exception("Incorrect id");
            context.Flights.Remove(flight);
        }

        public async Task<Flight> Update(int id, Flight entity)
        {
            Flight flight =await GetById(id);
            if (flight == null)
                throw new System.Exception("Incorrect id");
            flight.ArrivalTime = entity.ArrivalTime;
            flight.DeparturePlace = entity.DeparturePlace;
            flight.DepartureTime = entity.DepartureTime;
            flight.Destination = entity.Destination;
            return flight;
        }
    }
}
