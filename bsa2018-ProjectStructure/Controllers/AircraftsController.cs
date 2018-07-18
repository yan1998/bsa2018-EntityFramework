using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/Aircrafts")]
    public class AircraftsController : Controller
    {
        private readonly IAircraftService aircraftService;

        public AircraftsController(IAircraftService aircraftService)
        {
            this.aircraftService = aircraftService;
        }

        // GET: api/Aircrafts
        [HttpGet]
        public async Task<JsonResult> Get()
        { 
            return Json(await aircraftService.GetAllAircrafts());
        }

        // GET: api/Aircrafts/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await aircraftService.GetAircraft(id));
        }
        
        // POST: api/Aircrafts
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]AircraftDTO aircraft)
        {
            try
            {
                return Json(await aircraftService.AddAircraft(aircraft));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/Aircrafts/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]AircraftDTO aircraft)
        {
            try
            {
                HttpContext.Response.StatusCode = 204;
                return Json(await aircraftService.UpdateAircraft(id, aircraft));
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
                HttpContext.Response.StatusCode = 204;
                await aircraftService.DeleteAircraft(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}
