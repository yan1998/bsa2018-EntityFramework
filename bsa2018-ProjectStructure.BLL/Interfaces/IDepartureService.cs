using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface IDepartureService
    {
        Task<DepartureDTO> AddDeparture(DepartureDTO departure);
        Task<List<DepartureDTO>> GetAllDepartures();
        Task<DepartureDTO> GetDeparture(int id);
        Task<DepartureDTO> UpdateDeparture(int id, DepartureDTO departure);
        Task DeleteDeparture(int id);
    }
}
