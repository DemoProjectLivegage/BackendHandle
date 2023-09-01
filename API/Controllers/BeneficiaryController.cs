using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Escrow_schedule;
using Application.DTO;
using AutoMapper;

namespace API.Controllers
{
    public class BeneficiaryController : BaseAPIController
    {
        private readonly IMapper _mapper;

        public BeneficiaryController(IMapper mapper)
        {
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<EscrowBeneficiaryDTO>>> GetBeneficiary()
        {

            List<Benificiary> list = await Mediator.Send(new Escrow_GetBenificiary.Query());
            List<EscrowBeneficiaryDTO> newList = _mapper.Map<List<EscrowBeneficiaryDTO>>(list);
            return newList;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBeneficiary(List<Benificiary> benificiary)
        {

            return Ok(await Mediator.Send(new Escrow_Benificiary.Command { benificiary = benificiary }));
        }
        [HttpGet("{id}")]
        public async Task<List<EscrowBeneficiaryDTO>> GetBenificiaryByLoanId(int id)
        {
            List<Benificiary> list = await Mediator.Send(new GetBeneficiaryByLoanId.Query { Id = id });
            List<EscrowBeneficiaryDTO> newList = _mapper.Map<List<EscrowBeneficiaryDTO>>(list);


            return newList;
        }
    }
}