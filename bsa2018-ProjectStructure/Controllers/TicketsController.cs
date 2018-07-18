using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.Controllers
{
    [Produces("application/json")]
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
        private readonly ITicketService ticketService;

        public TicketsController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await ticketService.GetAllTickets());
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await ticketService.GetTicket(id));
        }
        
        // POST: api/Tickets
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]TicketDTO ticket)
        {
            try
            {
                return Json(await ticketService.AddTicket(ticket));
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        
        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody]TicketDTO ticket)
        {
            try
            {
                return Json(await ticketService.UpdateTicket(id, ticket));
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
                await ticketService.DeleteTicket(id);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}
