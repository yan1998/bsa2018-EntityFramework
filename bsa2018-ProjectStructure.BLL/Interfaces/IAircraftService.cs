using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface IAircraftService
    {
        Task<AircraftDTO> AddAircraft(AircraftDTO aircraft);
        Task<List<AircraftDTO>> GetAllAircrafts();
        Task<AircraftDTO> GetAircraft(int id);
        Task<AircraftDTO> UpdateAircraft(int id, AircraftDTO aircraft);
        Task DeleteAircraft(int id);
    }
}
