using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface IAircraftTypesService
    {
        Task<AircraftTypeDTO> AddAircraftType(AircraftTypeDTO aircraftType);
        Task<List<AircraftTypeDTO>> GetAllAircraftTypes();
        Task<AircraftTypeDTO> GetAircraftType(int id);
        Task<AircraftTypeDTO> UpdateAircraftType(int id, AircraftTypeDTO aircraftType);
        Task DeleteAircraftType(int id);
    }
}
