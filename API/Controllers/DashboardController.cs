using Application.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DashboardController : BaseAPIController
    {
       

        [HttpGet("bar/{Id}")]
        public async Task<ActionResult> GetBarChartDetails(int Id){
            return Ok(await Mediator.Send( new BarChartDetails.Query{LoanId=Id}));
        }

        [HttpGet("pie/{Id}")]
        public async Task<ActionResult> GetPieChartDetails(int Id){
            return Ok(await Mediator.Send( new PieChartDetails.Query{LoanId=Id}));
        }
    }
}