using API.Controllers.DTO;
using Application.DTO;
using Application.Escrow_schedule;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EscrowDisbursementController : BaseAPIController
    {
        private readonly IMapper _mapper;

        public EscrowDisbursementController(IMapper mapper)
        {
            _mapper = mapper;
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateDisbursement(LoanBeneficiaryDTO dto) {
            return Ok(await Mediator.Send(new CreateDisbursementService.Command{loanBeneficiaryDTO = dto}));
        }

        [HttpGet]
        public async Task<ActionResult<List<EscrowDisbursementDTO>>> getEscrowDisbursement() {
            List<Escrow_Disbursement_Schedule> list =  await Mediator.Send(new GetEscrowDisbursement.Query{});
            List<EscrowDisbursementDTO> newList = _mapper.Map<List<EscrowDisbursementDTO>>(list);
            return newList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EscrowDisbursementDTO>>> getEscrowById(int id) {
            List<Escrow_Disbursement_Schedule> list =   await Mediator.Send(new GetEscrowById.Query{Id = id});

            if(list.Count()==0)  {

                return NotFound();
            }
            List<EscrowDisbursementDTO> newList = _mapper.Map<List<EscrowDisbursementDTO>>(list);
            return newList;
        }
    }
}