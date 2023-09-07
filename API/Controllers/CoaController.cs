using Application.GL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CoaController : BaseAPIController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCoa([FromBody] COA data)
        {
            await Mediator.Send(new CreateCOA.Command { coa = data });
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCOA( int id)
        {
            return Ok(await Mediator.Send(new ShowAllGL.Query{Id=id}));
        }
    }
}