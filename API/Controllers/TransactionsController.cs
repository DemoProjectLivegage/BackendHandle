using Application.GL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionsController : BaseAPIController
    {
        

        [HttpPost]
        public IActionResult CreateTransactions([FromBody] Transactions transaction){
  

            return Ok(Mediator.Send(new AllTransaction.Command{transaction=transaction}));
        }
        [HttpGet]
        public async Task<ActionResult<Transactions>> GetTransaction()
        {
            return Ok(await Mediator.Send(new GetTransaction.Query()));
        }
    }
}