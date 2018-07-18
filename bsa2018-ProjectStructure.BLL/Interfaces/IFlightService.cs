using System.Collections.Generic;
using System.Threading.Tasks;
using bsa2018_ProjectStructure.Shared.DTO;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface IFlightService
    {
        Task<FlightDTO> AddFlight(FlightDTO flight);
        Task<List<FlightDTO>> GetAllFlights();
        Task<FlightDTO> GetFlight(int id);
        Task<FlightDTO> UpdateFlight(int id,FlightDTO flight);
        Task DeleteFlight(int id);
    }
}
