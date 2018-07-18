
using bsa2018_ProjectStructure.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using bsa2018_ProjectStructure.Shared.DTO;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Route("api/Flights")]
    public class FlightsController : Controller
    {
        private readonly IFlightService flightService;

        public FlightsController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        // GET: api/flights
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await flightService.GetAllFlights());
        }

        // GET: api/flights/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await flightService.GetFlight(id));
        }

        // POST api/flights
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]FlightDTO flight)
        {
            try
            {
                return Json(await flightService.AddFlight(flight));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }

        // PUT api/flights/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]FlightDTO flight)
        {
            try
            {
                return Json(await flightService.UpdateFlight(id, flight));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 404;
                return Json(ex.Message);
            }
        }

        // DELETE api/flights/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await flightService.DeleteFlight(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}