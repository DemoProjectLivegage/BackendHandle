using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.LoansDetails;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LoandetailsController :   BaseAPIController
    {
       
    
        [HttpGet] 
        public async Task<ActionResult<List<LoanDetails>>> GetActivities()
        {
             return await Mediator.Send(new List.Query());
        }
    }
    }
