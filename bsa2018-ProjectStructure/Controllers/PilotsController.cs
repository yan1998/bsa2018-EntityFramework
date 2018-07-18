using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/Pilots")]
    public class PilotsController : Controller
    {
        private readonly IPilotService pilotService;

        public PilotsController(IPilotService pilotService)
        {
            this.pilotService = pilotService;
        }

        // GET: api/Pilots
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await pilotService.GetAllPilots());
        }

        // GET: api/Pilots/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await pilotService.GetPilot(id));
        }
        
        // POST: api/Pilots
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]PilotDTO pilot)
        {
            try
            {
                return Json(await pilotService.AddPilot(pilot));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/Pilots/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]PilotDTO pilot)
        {
            try
            {
                return Json(await pilotService.UpdatePilot(id, pilot));
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
                await pilotService.DeletePilot(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}
