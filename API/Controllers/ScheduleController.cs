using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Escrow_schedule;

namespace API.Controllers
{
    public class ScheduleController :BaseAPIController
    {
 
        [HttpPost("{id}")]

        // public async Task<LoanInformation> GetScheduleDetails(int id)
        // {
        //     return await Mediator.Send(new Schedule.Query { LoanId = id });
        // }
         public async Task<Unit> ScheduleDetails(int id)
        {
             await Mediator.Send(new Schedule.Command { LoanInformationId = id });
            return Unit.Value;
        }

    }
}