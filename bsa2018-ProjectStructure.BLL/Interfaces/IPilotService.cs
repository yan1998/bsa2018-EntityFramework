using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface IPilotService
    {
        Task<PilotDTO> AddPilot(PilotDTO pilot);
        Task<List<PilotDTO>> GetAllPilots();
        Task<PilotDTO> GetPilot(int id);
        Task<PilotDTO> UpdatePilot(int id, PilotDTO pilot);
        Task DeletePilot(int id);
    }
}
