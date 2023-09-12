using Application.GL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionsController : BaseAPIController
    {
        

        [HttpPost]
        public async Task<ActionResult> CreateTransactions( Transactions transaction){
  
            return Ok(await Mediator.Send(new AllTransaction.Command{transaction=transaction}));
        }
        [HttpGet]
        public async Task<ActionResult<Transactions>> GetTransaction()
        {
            var s = await Mediator.Send(new GetTransaction.Query());
            return Ok();
        }
    }
}