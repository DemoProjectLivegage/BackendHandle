using Application.GL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GeneralLedgerController : BaseAPIController
    {
        [HttpPost]
        public async Task<IActionResult> CreateGL([FromBody] GeneralLedger gl)
        {
            return Ok(await Mediator.Send(new CreateGL.Command { data = gl }));
        }
        [HttpGet]
        public async Task<IActionResult> GetGL()
        {
            return Ok(await Mediator.Send(new GetAllGL.Query()));
        }
        
    }
}