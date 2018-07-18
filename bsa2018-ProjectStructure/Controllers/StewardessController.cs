using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/Stewardess")]
    public class StewardessController : Controller
    {
        private readonly IStewardessService stewardessService;

        public StewardessController(IStewardessService stewardessService)
        { 
            this.stewardessService = stewardessService;
        }

        // GET: api/Stewardess
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await stewardessService.GetAllStewardess());
        }

        // GET: api/Stewardess/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await stewardessService.GetStewardess(id));
        }
        
        // POST: api/Stewardess
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]StewardessDTO stewardess)
        {
            try
            {
                return Json(await stewardessService.AddStewardess(stewardess));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/Stewardess/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]StewardessDTO stewardess)
        {
            try
            {
                return Json(await stewardessService.UpdateStewardess(id, stewardess));
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
                await stewardessService.DeleteStewardess(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}
