using AutoMapper;
using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.BLL.Validators;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using bsa2018_ProjectStructure.DataAccess.Model;
using bsa2018_ProjectStructure.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Services
{
    public class TicketService:ITicketService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly TicketValidator validator;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new TicketValidator();
        }

        public async Task<TicketDTO> AddTicket(TicketDTO ticket)
        {
            Validation(ticket);
            Ticket modelTicket = mapper.Map<TicketDTO, Ticket>(ticket);
            Ticket result = await unitOfWork.Tickets.Create(modelTicket);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Ticket, TicketDTO>(result);
        }

        public async Task DeleteTicket(int id)
        {
            try
            {
                await unitOfWork.Tickets.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TicketDTO>> GetAllTickets()
        {
            IEnumerable<Ticket> tickets = await unitOfWork.Tickets.GetAll();
            return mapper.Map<IEnumerable<Ticket>, List<TicketDTO>>(tickets);
        }

        public async Task<TicketDTO> GetTicket(int id)
        {
            Ticket ticket = await unitOfWork.Tickets.GetById(id);
            return mapper.Map<Ticket, TicketDTO>(ticket);
        }

        public async Task<TicketDTO> UpdateTicket(int id, TicketDTO ticket)
        {
            try
            {
                Validation(ticket);
                Ticket modelTicket = mapper.Map<TicketDTO, Ticket>(ticket);
                Ticket result = await unitOfWork.Tickets.Update(id, modelTicket);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<Ticket, TicketDTO>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Validation(TicketDTO ticket)
        {
            var validationResult = validator.Validate(ticket);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
