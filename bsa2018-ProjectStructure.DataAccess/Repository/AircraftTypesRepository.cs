using bsa2018_ProjectStructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public class AircraftTypesRepository : IRepository<AircraftType>
    {
        protected readonly DataContext context;

        public AircraftTypesRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<AircraftType> Create(AircraftType entity)
        {
            await context.AircraftTypes.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            AircraftType aircraftType = await GetById(id);
            if (aircraftType == null)
                throw new System.Exception("Incorrect id");
            context.AircraftTypes.Remove(aircraftType);
        }

        public async Task<IEnumerable<AircraftType>> GetAll()
        {
            return await context.AircraftTypes.ToListAsync();
        }

        public async Task<AircraftType> GetById(int id)
        {
            return await context.AircraftTypes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<AircraftType> Update(int id, AircraftType entity)
        {
            AircraftType aircraftType =await GetById(id);
            if (aircraftType == null)
                throw new System.Exception("Incorrect id");
            aircraftType.LoadCapacity = entity.LoadCapacity;
            aircraftType.Places = entity.Places;
            return aircraftType;
        }
    }
}
