using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.PaymentScheduleService;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentScheduleController : BaseAPIController
    {
        private readonly IMapper _mapper;
        public PaymentScheduleController(IMapper mapper)
        {
            _mapper = mapper;
            
        }
        [HttpGet] //api/paymentschedule
        public async Task<ActionResult<List<PaymentDTO>>> GetPaymentSchedule() {
            List<Payment_Schedule> list =  await Mediator.Send(new PaymentSchedule.Query());

            List<PaymentDTO> newList = _mapper.Map<List<PaymentDTO>>(list);
            return newList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PaymentDTO>>> getPaymentScheduleById(int id) {
            List<Payment_Schedule> list =  await Mediator.Send(new PaymentScheduleById.Query{Loan_Id = id});

            List<PaymentDTO> newList =  _mapper.Map<List<PaymentDTO>>(list);

           
            return newList;
        }
    }
}