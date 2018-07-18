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
    public class AircraftTypesService: IAircraftTypesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AircraftTypeValidator validator;

        public AircraftTypesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new AircraftTypeValidator();
        }

        public async Task<AircraftTypeDTO> AddAircraftType(AircraftTypeDTO aircraftType)
        {
            Validation(aircraftType);
            AircraftType modelAircraftType = mapper.Map<AircraftTypeDTO, AircraftType>(aircraftType);
            AircraftType result = await unitOfWork.AircraftTypes.Create(modelAircraftType);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AircraftType, AircraftTypeDTO>(result);
        }

        public async Task DeleteAircraftType(int id)
        {
            try
            {
                await unitOfWork.AircraftTypes.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AircraftTypeDTO> GetAircraftType(int id)
        {
            AircraftType aircraftType = await unitOfWork.AircraftTypes.GetById(id);
            return mapper.Map<AircraftType, AircraftTypeDTO>(aircraftType);
        }

        public async Task<List<AircraftTypeDTO>> GetAllAircraftTypes()
        {
            IEnumerable<AircraftType> aircraftsTypes = await unitOfWork.AircraftTypes.GetAll();
            return mapper.Map<IEnumerable<AircraftType>, List<AircraftTypeDTO>>(aircraftsTypes);
        }

        public async Task<AircraftTypeDTO> UpdateAircraftType(int id, AircraftTypeDTO aircraftType)
        {
            try
            {
                Validation(aircraftType);
                AircraftType modelAircraftTypes = mapper.Map<AircraftTypeDTO, AircraftType>(aircraftType);
                AircraftType result = await unitOfWork.AircraftTypes.Update(id, modelAircraftTypes);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<AircraftType, AircraftTypeDTO>(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void Validation(AircraftTypeDTO aircraftType)
        {
            var validationResult = validator.Validate(aircraftType);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
