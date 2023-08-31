using Microsoft.AspNetCore.Mvc;
using Application.Payment_Hierarchy_;
using Domain;
using Application.DTO;

namespace API.Controllers
{
    public class Payment_HierarchyController : BaseAPIController
    {
        [HttpPost]

        public async Task<IActionResult> GetHierarchy(int id, DateOnly date, decimal incoming_amount)
        {
            try
            {
                return Ok(await Mediator.Send(new Payment_Hierarchy_Application.Command { id = id, payment_date = date, incoming_amount = incoming_amount }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<List<TransactionDTO>> GetAllHierarchy(int id)
        {
            var hierarchy = await Mediator.Send(new Payment_Hierarchy_byid.Query { Id = id });
            List<TransactionDTO> list = new List<TransactionDTO>();

            foreach (var item in hierarchy)
            {
                TransactionDTO dto = new TransactionDTO();
                dto.TransactionDate = item.date;
                dto.SheduledAmount = item.Monthly_Payment_Amount;
                dto.ReceivedAmount = item.actual_receive;
                dto.InterestAmount = item.interest;
                dto.PrincipalAmount = item.principal;
                dto.EscrowAmount = item.escrow;
                dto.LateCharges = item.late_charge;
                dto.OtherFees = item.other_fee;
                dto.Suspense = item.suspence;
                dto.UPBAmount = item.UPB_Amount;
                list.Add(dto);
            }

            return list;
        }
    }
}