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
    public class CrewService : ICrewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CrewValidator validator;

        public CrewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new CrewValidator();
        }

        public async Task<CrewDTO> AddCrew(CrewDTO crew)
        {
            Validation(crew);
            Crew modelCrew = mapper.Map<CrewDTO, Crew>(crew);
            Crew result = await unitOfWork.Crews.Create(modelCrew);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Crew, CrewDTO>(result);
        }

        public async Task DeleteCrew(int id)
        {
            try
            {
                await unitOfWork.Crews.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CrewDTO>> GetAllCrews()
        {
            IEnumerable<Crew> crews = await unitOfWork.Crews.GetAll();
            return mapper.Map<IEnumerable<Crew>, List<CrewDTO>>(crews);
        }

        public async Task<CrewDTO> GetCrew(int id)
        {
            Crew crew = await unitOfWork.Crews.GetById(id);
            return mapper.Map<Crew, CrewDTO>(crew);
        }

        public async Task<CrewDTO> UpdateCrew(int id, CrewDTO crew)
        {
            try
            {
                Validation(crew);
                Crew modelCrew = mapper.Map<CrewDTO, Crew>(crew);
                Crew result = await unitOfWork.Crews.Update(id, modelCrew);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<Crew, CrewDTO>(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void Validation(CrewDTO crew)
        {
            var validationResult = validator.Validate(crew);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
