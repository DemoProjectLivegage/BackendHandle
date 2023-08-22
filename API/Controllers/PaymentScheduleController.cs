using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.PaymentScheduleService;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentScheduleController : BaseAPIController
    {
        [HttpGet] //api/paymentschedule
        public async Task<ActionResult<List<Payment_Schedule>>> GetPaymentSchedule() {
            return await Mediator.Send(new PaymentSchedule.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Payment_Schedule>>> getPaymentScheduleById(int id) {
            return await Mediator.Send(new PaymentScheduleById.Query{Loan_Id = id});
        }
    }
}