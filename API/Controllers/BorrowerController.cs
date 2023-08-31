using Application.Borrower;
using Application.DTO;
using Application.LoanInformations;
using Application.LoansDetails;
using Application.PaymentScheduleService;
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
            // int id = 1;
            // Payment_Schedule payment_Schedule;
            // await Mediator.Send(new Payment_Schedule_Service.Command(Loan_Id, payment_Schedule));
            return Ok(await Mediator.Send(new Create.Command { file = tempFilePath }));
        }

        [HttpGet("dynamic/")]

        public async Task<ActionResult<dynamic_details>> GetLoanDetails(int id,DateOnly date)
        {
            return await Mediator.Send(new Loandetail.Query { LoanInformationId = id , due_date=date });
        }

        [HttpGet("loaninfo/{id}")]
        public async Task<ActionResult<LoanInformation>> GetLoanInfo(int Id)
        {
            return await Mediator.Send(new Loaninfodetail.Query { BorrowerId = Id });
        }


        [HttpGet("loan_details/{id}")]
        public async Task<ActionResult<LoanDetails>> GetLoanDetail(int Id)
        {
            return await Mediator.Send(new LoanDetailsAPI.Query { LoanId = Id });
        }
    }
}