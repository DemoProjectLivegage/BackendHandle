
using Application.Borrower;
using Domain;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class BorrowerController : BaseAPIController
    {
         [HttpGet] //api/activities
        public async Task<ActionResult<List<BorrowerDetails>>> GetActivities()
        {
             return await Mediator.Send(new List.Query());
        }

         [HttpPost]
        public async Task<IActionResult> CreateActivity()
        {
            return Ok( await Mediator.Send(new Create.Command()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowerDetails>> GetBorrowers(int Id)
        
        {
            return await Mediator.Send(new Details.Query{BorrowerId=Id});
        }
    }
}