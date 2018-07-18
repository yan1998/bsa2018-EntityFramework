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
    public class PilotService:IPilotService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PilotValidator validator;

        public PilotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new PilotValidator();
        }

        public async Task<PilotDTO> AddPilot(PilotDTO pilot)
        {
            Validation(pilot);
            Pilot modelPilot = mapper.Map<PilotDTO, Pilot>(pilot);
            Pilot result = await unitOfWork.Pilots.Create(modelPilot);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Pilot, PilotDTO>(result);
        }

        public async Task DeletePilot(int id)
        {
            try
            {
                await unitOfWork.Pilots.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PilotDTO>> GetAllPilots()
        {
            IEnumerable<Pilot> pilots = await unitOfWork.Pilots.GetAll();
            return mapper.Map<IEnumerable<Pilot>, List<PilotDTO>>(pilots);
        }

        public async Task<PilotDTO> GetPilot(int id)
        {
            Pilot pilot = await unitOfWork.Pilots.GetById(id);
            return mapper.Map<Pilot, PilotDTO>(pilot);
        }

        public async Task<PilotDTO> UpdatePilot(int id, PilotDTO pilot)
        {
            try
            {
                Validation(pilot);
                Pilot modelPilot = mapper.Map<PilotDTO, Pilot>(pilot);
                Pilot result = await unitOfWork.Pilots.Update(id, modelPilot);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<Pilot, PilotDTO>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Validation(PilotDTO pilot)
        {
            var validationResult = validator.Validate(pilot);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
