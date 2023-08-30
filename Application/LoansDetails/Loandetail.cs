using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.LoansDetails
{
  public class Loandetail
  {
    public class Query : IRequest<dynamic_details>
    {
      public int LoanInformationId { get; set; }
      public DateOnly due_date {get;set;}
    }

    public class Handler : IRequestHandler<Query, dynamic_details>
    {

      private readonly DatabaseContext _context;
      public Handler(DatabaseContext context)
      {
        _context = context;
      }
      public async Task<dynamic_details> Handle(Query request, CancellationToken cancellationToken)
      {
        var detail =await _context.Payment_Schedule.Where
        (l => l.Loan_Id == request.LoanInformationId &&  l.Due_Date == request.due_date).FirstOrDefaultAsync();
      

        dynamic_details dto=new dynamic_details();

        dto.UPB_Amount=detail.UPB_Amount;
        dto.Due_Date=detail.Due_Date;
        dto.RemainingPayments=detail.RemainingPayments;
        dto.Monthly_Payment_Amount=detail.Monthly_Payment_Amount;       

      
        return dto;
        // return await _context.LoanDetails.FindAsync(request.LoanInformationId);
      }
    }
  }
}