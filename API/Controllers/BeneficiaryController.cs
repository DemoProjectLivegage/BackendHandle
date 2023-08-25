using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Borrower;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Escrow_schedule;
using Azure.Core;
using Application.DTO;

namespace API.Controllers
{
    public class BeneficiaryController :BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<List<EscrowBeneficiaryDTO>>> GetBeneficiary(){

            List<Benificiary> list =  await Mediator.Send(new Escrow_GetBenificiary.Query());
            List<EscrowBeneficiaryDTO> newList = new List<EscrowBeneficiaryDTO>();

            foreach (var item in list)
            {
                EscrowBeneficiaryDTO dto = new EscrowBeneficiaryDTO();
                dto.escrow_type = item.escrow_type;
                dto.name = item.name;
                dto.account_no = item.account_no;
                dto.routing_no = item.routing_no;
                dto.payment_mode = item.payment_mode;
                dto.frequency = item.frequency;
                newList.Add(dto);  
            }

            return newList;
        }
         [HttpPost]
         public async Task<IActionResult> CreateBeneficiary(List<Benificiary> benificiary)
         {
            
            return Ok(await Mediator.Send(new Escrow_Benificiary.Command { benificiary =   benificiary}));
         }
        [HttpGet("{id}")]
        public async Task<List<EscrowBeneficiaryDTO>> GetBenificiaryByLoanId(int id) {
            List<Benificiary> list =  await Mediator.Send(new GetBeneficiaryByLoanId.Query{Id = id});
            List<EscrowBeneficiaryDTO> newList = new List<EscrowBeneficiaryDTO>();

            foreach (var item in list)
            {
                EscrowBeneficiaryDTO dto = new EscrowBeneficiaryDTO();
                dto.escrow_type = item.escrow_type;
                dto.name = item.name;
                dto.account_no = item.account_no;
                dto.routing_no = item.routing_no;
                dto.payment_mode = item.payment_mode;
                dto.frequency = item.frequency;
                newList.Add(dto);  
            }

            return newList;
        }
    }
}