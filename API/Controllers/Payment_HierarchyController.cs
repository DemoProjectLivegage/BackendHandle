using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Application.Payment_Hierarchy_;
using Domain;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<Payment_Hierarchy>> GetAllHierarchy(int id)
    {
        return await  Mediator.Send(new Payment_Hierarchy_byid.Query { Id = id });
    }


}
}