using Application.DTO;
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
        var detail =await _context.LoanDetails.Where
        (l => l.LoanId == request.LoanInformationId ).FirstOrDefaultAsync();
      

        dynamic_details dto=new dynamic_details();

        dto.UPB_Amount=detail.UPBAmt;
        dto.Due_Date=detail.PmtDueDate;
        dto.RemainingPayments=detail.RemainingPayments;
        // dto.Monthly_Payment_Amount=detail.Monthly_Payment_Amount;
        dto.loan_info_id = detail.LoanId;  

      
        return dto;
        // return await _context.LoanDetails.FindAsync(request.LoanInformationId);
      }
    }
  }
}