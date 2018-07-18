using bsa2018_ProjectStructure.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Interfaces
{
    public interface ICrewService
    {
        Task<CrewDTO> AddCrew(CrewDTO crew);
        Task<List<CrewDTO>> GetAllCrews();
        Task<CrewDTO> GetCrew(int id);
        Task<CrewDTO> UpdateCrew(int id, CrewDTO crew);
        Task DeleteCrew(int id);
    }
}
