using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.DTO;
using Application.Escrow_schedule;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EscrowDisbursementController : BaseAPIController
    {
        [HttpPost]
        public async Task<IActionResult> CreateDisbursement(LoanBeneficiaryDTO dto) {
            return Ok(await Mediator.Send(new CreateDisbursementService.Command{loanBeneficiaryDTO = dto}));
        }

        [HttpGet]
        public async Task<ActionResult<List<Escrow_Disbursement_Schedule>>> getEscrowDisbursement() {
            return await Mediator.Send(new GetEscrowDisbursement.Query{});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Escrow_Disbursement_Schedule>>> getEscrowById(int id) {
            return await Mediator.Send(new GetEscrowById.Query{Id = id});
        }
    }
}