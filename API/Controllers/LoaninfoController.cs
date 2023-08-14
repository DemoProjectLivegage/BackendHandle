using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.LoanInformations;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LoaninfoController : BaseAPIController
    {
         [HttpGet]
        public async Task<ActionResult<List<LoanInformation>>> GetActivities(){
       return await Mediator.Send(new List.Query());
        }
    }
}