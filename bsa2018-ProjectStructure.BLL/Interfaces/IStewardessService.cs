using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface IStewardessService
    {
        Task<StewardessDTO> AddStewardess(StewardessDTO stewardess);
        Task<List<StewardessDTO>> GetAllStewardess();
        Task<StewardessDTO> GetStewardess(int id);
        Task<StewardessDTO> UpdateStewardess(int id, StewardessDTO stewardess);
        Task DeleteStewardess(int id);
    }
}
