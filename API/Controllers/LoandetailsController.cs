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
             return await Mediator.Send(new LoanDetailList.Query());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<LoanDetails>> GetLoanDetails(int id)
        {
            return await Mediator.Send(new Loandetail.Query{LoanInformationId=id});
        }
    }
    }
