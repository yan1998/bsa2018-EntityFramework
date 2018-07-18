using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/Crews")]
    public class CrewsController : Controller
    {
        private readonly ICrewService crewService;

        public CrewsController(ICrewService crewService)
        {
            this.crewService = crewService;
        }

        // GET: api/Crews
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await crewService.GetAllCrews());
        }

        // GET: api/Crews/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await crewService.GetCrew(id));
        }
        
        // POST: api/Crews
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]CrewDTO value)
        {
            try
            {
                return Json(await crewService.AddCrew(value));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/Crews/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]CrewDTO crew)
        {
            try
            {
                return Json(await crewService.UpdateCrew(id, crew));
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
                await crewService.DeleteCrew(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}
