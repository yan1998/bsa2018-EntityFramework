using System.Collections.Generic;
using System.Linq;
using bsa2018_ProjectStructure.DataAccess.Model;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class FlightsRepository : IRepository<Flight>
    {
        protected readonly DataContext context;

        public FlightsRepository(DataContext context)
        {
            this.context = context;
        }

        IEnumerable<Flight> IRepository<Flight>.GetAll()
        {
            return context.Flights.ToList();
        }

        public Flight GetById(int id)
        {
            return context.Flights.FirstOrDefault(f => f.Id == id);
        }

        public Flight Create(Flight entity)
        {
            context.Flights.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            Flight flight = GetById(id);
            if (flight == null)
                throw new System.Exception("Incorrect id");
            context.Flights.Remove(flight);
        }

        public Flight Update(int id, Flight entity)
        {
            Flight flight = GetById(id);
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
