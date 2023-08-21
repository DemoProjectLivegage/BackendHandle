using Application.Borrower;
using Application.LoanInformations;
using Application.LoansDetails;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BorrowerController : BaseAPIController
    {
        [HttpGet] //api/borrower
        public async Task<ActionResult<List<BorrowerDetailsWithLoanInfo>>> GetBorrowers()
        {
            return await Mediator.Send(new List.Query());
        }



        [HttpPost]
        public async Task<IActionResult> CreateBorrower()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileData = Request.Form.Files[0];
            // var stream = new FileStream();
            FileStream stream = null;
            var tempFilePath = "";
            try
            {
                if (fileData != null && fileData.Length > 0)
                {
                    tempFilePath = Path.GetTempFileName();
                    using (stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await fileData.CopyToAsync(stream);

                    }
                }
            }catch(Exception e){
                Console.WriteLine("\n\nError"+e.Message+"\n\n");
                return BadRequest("Error");
            }
            return Ok(await Mediator.Send(new Create.Command { file = tempFilePath }));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<LoanDetails>> GetLoanDetails(int id)
        {
            return await Mediator.Send(new Loandetail.Query { LoanInformationId = id });
        }

        [HttpGet("loaninfo/{id}")]
        public async Task<ActionResult<LoanInformation>> GetLoanInfo(int Id)
        {
            return await Mediator.Send(new Loaninfodetail.Query { BorrowerId = Id });
        }
    }
}