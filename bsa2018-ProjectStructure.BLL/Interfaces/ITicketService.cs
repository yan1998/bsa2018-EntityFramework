using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDTO> AddTicket(TicketDTO ticket);
        Task<List<TicketDTO>> GetAllTickets();
        Task<TicketDTO> GetTicket(int id);
        Task<TicketDTO> UpdateTicket(int id, TicketDTO ticket);
        Task DeleteTicket(int id);
    }
}
