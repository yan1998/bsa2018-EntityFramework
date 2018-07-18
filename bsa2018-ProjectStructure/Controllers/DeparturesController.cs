using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/Departures")]
    public class DeparturesController : Controller
    {
        private readonly IDepartureService departureService;

        public DeparturesController(IDepartureService departureService)
        {
            this.departureService = departureService;
        }

        // GET: api/Departures
        [HttpGet]
        [HttpGet("{id}")]
        public async Task<JsonResult> Get()
        {
            return Json(await departureService.GetAllDepartures());
        }

        // GET: api/Departures/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await departureService.GetDeparture(id));
        }
        
        // POST: api/Departures
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]DepartureDTO departure)
        {
            try
            {
                return Json(await departureService.AddDeparture(departure));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/Departures/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]DepartureDTO departure)
        {
            try
            {
                return Json(await departureService.UpdateDeparture(id, departure));
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
                await departureService.DeleteDeparture(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}
