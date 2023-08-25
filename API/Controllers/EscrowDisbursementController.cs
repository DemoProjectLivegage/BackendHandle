using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.DTO;
using Application.DTO;
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
        public async Task<ActionResult<List<EscrowDisbursementDTO>>> getEscrowDisbursement() {
            List<Escrow_Disbursement_Schedule> list =  await Mediator.Send(new GetEscrowDisbursement.Query{});
            List<EscrowDisbursementDTO> newList = new List<EscrowDisbursementDTO>();

            foreach (var item in list)
            {
                EscrowDisbursementDTO dto = new EscrowDisbursementDTO();
                dto.beneficiary_id = item.beneficiary_id;
                dto.date = item.date;
                dto.escrow_payment_amount = "$" + item.escrow_payment_amount.ToString();
                dto.escrow_disbursement = "$" + item.escrow_disbursement;
                dto.Escrow_Name = item.Escrow_Name;
                dto.Escrow_Balance = "$" + item.Escrow_Balance.ToString();
                dto.disbursement_frequency = item.disbursement_frequency;
                newList.Add(dto);
            }

            return newList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EscrowDisbursementDTO>>> getEscrowById(int id) {
            List<Escrow_Disbursement_Schedule> list =  await Mediator.Send(new GetEscrowById.Query{Id = id});
            List<EscrowDisbursementDTO> newList = new List<EscrowDisbursementDTO>();

            foreach (var item in list)
            {
                EscrowDisbursementDTO dto = new EscrowDisbursementDTO();
                dto.beneficiary_id = item.beneficiary_id;
                dto.date = item.date;
                dto.escrow_payment_amount = "$" + item.escrow_payment_amount.ToString();
                dto.escrow_disbursement = "$" + item.escrow_disbursement;
                dto.Escrow_Name = item.Escrow_Name;
                dto.Escrow_Balance = "$" + item.Escrow_Balance.ToString();
                dto.disbursement_frequency = item.disbursement_frequency;
                newList.Add(dto);
            }

            return newList;
        }
    }
}