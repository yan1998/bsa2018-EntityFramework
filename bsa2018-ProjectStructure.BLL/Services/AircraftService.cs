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
    public class AircraftService : IAircraftService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AircraftValidator validator;

        public AircraftService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new AircraftValidator();
        }

        public async Task<AircraftDTO> AddAircraft(AircraftDTO aircraft)
        {
            Validation(aircraft);
            Aircraft modelAircraft = mapper.Map<AircraftDTO, Aircraft>(aircraft);
            Aircraft result=await unitOfWork.Aircrafts.Create(modelAircraft);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Aircraft, AircraftDTO>(result);
        }

        public async Task DeleteAircraft(int id)
        {
            try
            {
                await unitOfWork.Aircrafts.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AircraftDTO> GetAircraft(int id)
        {
            Aircraft aircraft = await unitOfWork.Aircrafts.GetById(id);
            return mapper.Map<Aircraft, AircraftDTO>(aircraft);
        }

        public async Task<List<AircraftDTO>> GetAllAircrafts()
        {
            IEnumerable<Aircraft> aircrafts = await unitOfWork.Aircrafts.GetAll();
            return mapper.Map<IEnumerable<Aircraft>, List<AircraftDTO>>(aircrafts);
        }

        public async Task<AircraftDTO> UpdateAircraft(int id, AircraftDTO aircraft)
        {
            try
            {
                Validation(aircraft);
                Aircraft modelAircraft = mapper.Map<AircraftDTO, Aircraft>(aircraft);
                Aircraft result = await unitOfWork.Aircrafts.Update(id, modelAircraft);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<Aircraft, AircraftDTO>(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void Validation(AircraftDTO aircraft)
        {
            var validationResult = validator.Validate(aircraft);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
