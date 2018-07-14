using System.Collections.Generic;
using System.Linq;
using bsa2018_ProjectStructure.DataAccess.Model;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class TicketsRepository : IRepository<Ticket>
    {
        protected readonly DataContext context;

        public TicketsRepository(DataContext context)
        {
            this.context = context;
        }

        public Ticket Create(Ticket entity)
        {
            context.Tickets.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            Ticket ticket = GetById(id);
            if (ticket == null)
                throw new System.Exception("Incorrect id");
            context.Tickets.Remove(ticket);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return context.Tickets.ToList();
        }

        public Ticket GetById(int id)
        {
            return context.Tickets.FirstOrDefault(t => t.Id == id);
        }

        public Ticket Update(int id, Ticket entity)
        {
            Ticket ticket = GetById(id);
            if (ticket == null)
                throw new System.Exception("Incorrect id");
            ticket.Cost = entity.Cost;
            ticket.IdFlight = entity.IdFlight;
            ticket.Flight = context.Flights.FirstOrDefault(f => f.Id == entity.IdFlight);
            return ticket;
        }
    }
}
