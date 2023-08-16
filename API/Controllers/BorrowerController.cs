using Application.Borrower;
using Application.LoanInformations;
using Application.LoansDetails;
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
            var fileData = Request.Form.Files[0];
            // var stream = new FileStream();
            FileStream stream = null;
            var tempFilePath = "";
            if (fileData != null && fileData.Length > 0)
            {
                tempFilePath = Path.GetTempFileName();
                Console.WriteLine("\n\n\n"+tempFilePath+"\n\n\n");
                using (stream = new FileStream(tempFilePath,FileMode.Create))
                {
                    await fileData.CopyToAsync(stream);
                    
                }
            }
            return Ok(await Mediator.Send(new Create.Command { file = tempFilePath }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowerDetails>> GetBorrowers(int Id)

        {
            return await Mediator.Send(new Details.Query { BorrowerId = Id });
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<LoanDetails>> GetLoanDetails(int id)
        {
            return await Mediator.Send(new Loandetail.Query{LoanInformationId=id});
        }

        [HttpGet("{id}")]
         public async Task<ActionResult<LoanInformation>> GetLoanInfo(int Id)
         {
            return await Mediator.Send(new Loaninfodetail.Query{BorrowerId=Id});
         }
    }
}