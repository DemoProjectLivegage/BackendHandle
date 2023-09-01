using Microsoft.AspNetCore.Mvc;
using Application.Payment_Hierarchy_;
using Domain;
using Application.DTO;
using AutoMapper;

namespace API.Controllers
{
    public class Payment_HierarchyController : BaseAPIController
    {
        public IMapper _mapper { get; }

        public Payment_HierarchyController(IMapper mapper)
        {
            _mapper = mapper;
            
        }
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
            List<TransactionDTO> list = _mapper.Map<List<TransactionDTO>>(hierarchy);
            return list;
        }
    }
}