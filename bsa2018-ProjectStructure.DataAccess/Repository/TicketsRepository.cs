using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class TicketsRepository : IRepository<Ticket>
    {
        protected readonly DataContext context;

        public TicketsRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Ticket> Create(Ticket entity)
        {
            await context.Tickets.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            Ticket ticket = await GetById(id);
            if (ticket == null)
                throw new System.Exception("Incorrect id");
            context.Tickets.Remove(ticket);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await context.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetById(int id)
        {
            return await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket> Update(int id, Ticket entity)
        {
            Ticket ticket = await GetById(id);
            if (ticket == null)
                throw new System.Exception("Incorrect id");
            ticket.Cost = entity.Cost;
            ticket.IdFlight = entity.IdFlight;
            ticket.Flight = context.Flights.FirstOrDefault(f => f.Id == entity.IdFlight);
            return ticket;
        }
    }
}
