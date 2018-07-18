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
    public class DepartureService:IDepartureService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly DepartureValidator validator;

        public DepartureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new DepartureValidator();
        }

        public async Task<DepartureDTO> AddDeparture(DepartureDTO departure)
        {
            Validation(departure);
            Departure modelDeparture = mapper.Map<DepartureDTO, Departure>(departure);
            Departure result = await unitOfWork.Departures.Create(modelDeparture);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Departure, DepartureDTO>(result);
        }

        public async Task DeleteDeparture(int id)
        {
            try
            {
                await unitOfWork.Departures.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DepartureDTO>> GetAllDepartures()
        {
            IEnumerable<Departure> departures = await unitOfWork.Departures.GetAll();
            return mapper.Map<IEnumerable<Departure>, List<DepartureDTO>>(departures);
        }

        public async Task<DepartureDTO> GetDeparture(int id)
        {
            Departure departure = await unitOfWork.Departures.GetById(id);
            return mapper.Map<Departure, DepartureDTO>(departure);
        }

        public async Task<DepartureDTO> UpdateDeparture(int id, DepartureDTO departure)
        {
            try
            {
                Validation(departure);
                Departure modelDeparture = mapper.Map<DepartureDTO, Departure>(departure);
                Departure result = await unitOfWork.Departures.Update(id, modelDeparture);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<Departure, DepartureDTO>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Validation(DepartureDTO departure)
        {
            var validationResult = validator.Validate(departure);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
