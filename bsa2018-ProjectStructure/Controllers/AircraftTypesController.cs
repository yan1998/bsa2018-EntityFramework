using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/AircraftTypes")]
    public class AircraftTypesController : Controller
    {
        private readonly IAircraftTypesService aircraftTypesService;

        public AircraftTypesController(IAircraftTypesService aircraftTypesService)
        {
            this.aircraftTypesService = aircraftTypesService;
        }

        // GET: api/AircraftTypes
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await aircraftTypesService.GetAllAircraftTypes());
        }

        // GET: api/AircraftTypes/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await aircraftTypesService.GetAircraftType(id));
        }
        
        // POST: api/AircraftTypes
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]AircraftTypeDTO aircraftType)
        {
            try
            {
                return Json(await aircraftTypesService.AddAircraftType(aircraftType));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/AircraftTypes/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]AircraftTypeDTO aircraftType)
        {
            try
            {
                return Json(await aircraftTypesService.UpdateAircraftType(id, aircraftType));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 404;
                return Json(ex.Message);
            }

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await aircraftTypesService.DeleteAircraftType(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }

        }
    }
}
