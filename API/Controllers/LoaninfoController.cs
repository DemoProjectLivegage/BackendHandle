using Application.LoanInformations;
using Domain;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class LoaninfoController : BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<List<LoanInformation>>> GetActivities()
        {
            return await Mediator.Send(new List.Query());
        }


        [HttpGet("{id}")]
         public async Task<ActionResult<LoanInformation>> GetLoanInfo(int Id)
         {
            return await Mediator.Send(new Loaninfodetail.Query{BorrowerId=Id});
         }
        

    }   
}