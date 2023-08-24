using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Borrower;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Escrow_schedule;
using Azure.Core;

namespace API.Controllers
{
    public class BeneficiaryController :BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<List<Benificiary>>> GetBeneficiary(){
            return await Mediator.Send(new Escrow_GetBenificiary.Query());
        }
         [HttpPost]
         public async Task<IActionResult> CreateBeneficiary(List<Benificiary> benificiary)
         {
            
            return Ok(await Mediator.Send(new Escrow_Benificiary.Command { benificiary =   benificiary}));
         }

    }
}