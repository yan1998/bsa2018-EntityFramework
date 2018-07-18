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
    public class StewardessService:IStewardessService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly StewardessValidator validator;

        public StewardessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new StewardessValidator();
        }

        public async Task<StewardessDTO> AddStewardess(StewardessDTO stewardess)
        {
            Validation(stewardess);
            Stewardess modelStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess);
            Stewardess result= await unitOfWork.Stewardess.Create(modelStewardess);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Stewardess, StewardessDTO>(result);
        }

        public async Task DeleteStewardess(int id)
        {
            try
            {
                await unitOfWork.Stewardess.Delete(id);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StewardessDTO>> GetAllStewardess()
        {
            IEnumerable<Stewardess> stewardesses = await unitOfWork.Stewardess.GetAll();
            return mapper.Map<IEnumerable<Stewardess>, List<StewardessDTO>>(stewardesses);
        }

        public async Task<StewardessDTO> GetStewardess(int id)
        {
            Stewardess stewardess = await unitOfWork.Stewardess.GetById(id);
            return mapper.Map<Stewardess, StewardessDTO>(stewardess);
        }

        public async Task<StewardessDTO> UpdateStewardess(int id, StewardessDTO stewardess)
        {
            try
            {
                Validation(stewardess);
                Stewardess modelStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess);
                Stewardess result = await unitOfWork.Stewardess.Update(id, modelStewardess);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<Stewardess, StewardessDTO>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        private void Validation(StewardessDTO stewardess)
        {
            var validationResult = validator.Validate(stewardess);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ToString());
        }
    }
}
