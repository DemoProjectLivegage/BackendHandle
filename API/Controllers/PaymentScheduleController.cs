using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.PaymentScheduleService;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentScheduleController : BaseAPIController
    {
        [HttpGet] //api/paymentschedule
        public async Task<ActionResult<List<PaymentDTO>>> GetPaymentSchedule() {
            List<Payment_Schedule> list =  await Mediator.Send(new PaymentSchedule.Query());

            List<PaymentDTO> newList = new List<PaymentDTO>();

            foreach (var item in list)
            {
                PaymentDTO dto = new PaymentDTO();
                dto.Due_Date = item.Due_Date;
                dto.Principal_Amount = "$"+item.Principal_Amount.ToString();
                dto.Interest_Amount = "$" + item.Interest_Amount.ToString();
                dto.Escrow_Amount = "$" + item.Escrow_Amount.ToString();
                dto.Monthly_Payment_Amount = "$" + item.Monthly_Payment_Amount.ToString();
                dto.UPB_Amount = "$" + item.UPB_Amount.ToString();
                dto.Note_Interest_Rate = item.Note_Interest_Rate;
                newList.Add(dto);
            }
            return newList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PaymentDTO>>> getPaymentScheduleById(int id) {
            List<Payment_Schedule> list =  await Mediator.Send(new PaymentScheduleById.Query{Loan_Id = id});

            List<PaymentDTO> newList = new List<PaymentDTO>();

            foreach (var item in list)
            {
                PaymentDTO dto = new PaymentDTO();
                dto.Due_Date = item.Due_Date;
                dto.Principal_Amount = "$" + item.Principal_Amount.ToString();
                dto.Interest_Amount = "$" + item.Interest_Amount.ToString();
                dto.Escrow_Amount = "$" + item.Escrow_Amount.ToString();
                dto.Monthly_Payment_Amount = "$" + item.Monthly_Payment_Amount.ToString();
                dto.UPB_Amount = "$" + item.UPB_Amount.ToString();
                dto.Note_Interest_Rate = item.Note_Interest_Rate;
                newList.Add(dto);
            }
            return newList;
        }
    }
}