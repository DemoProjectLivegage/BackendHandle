
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Borrower
{
    public class List
    {

      public class Query : IRequest<List<BorrowerDetailsWithLoanInfo>>{ }

        public class Handler : IRequestHandler<Query, List<BorrowerDetailsWithLoanInfo>>
        { 
            private readonly DatabaseContext _context;
           
            public Handler(DatabaseContext context)
            {
                _context=context;
              
            }
            public async Task<List<BorrowerDetailsWithLoanInfo>> Handle(Query request, CancellationToken cancellationToken)


            {
 
             var borrowerDetails= await _context.BorrowersDetails.Join(

                _context.LoanInformation,
                b=>b.BorrowerId,
                l=>l.BorrowerId,
                (_b,_l)=> new BorrowerDetailsWithLoanInfo{
                    FullName=_b.FullName,
                    LoanInformationId=_l.LoanInformationId,
                    PriorServicerLoanId=_l.PriorServicerLoanId,
                    ContactNumber=_b.ContactNumber,
                    MailingAddress=_b.MailingAddress,
                    Email=_b.Email,
                    Zipcode=_b.Zipcode,
                    Occupation=_b.Occupation,
                    BorrowerId=_b.BorrowerId
                }
             ).ToListAsync();
              
          
               return borrowerDetails;
           
        }
                
        }


        
    }

    public class BorrowerDetailsWithLoanInfo
        {

          public int BorrowerId {get; set;}
     public string FullName {get; set;}
      public int LoanInformationId {get;set;}
       public int PriorServicerLoanId { get; set; }

     public string ContactNumber { set; get; }

    public string MailingAddress { set; get; }

    public int Zipcode { get; set; }


    public string Email { get; set; }

    public string Occupation { get; set; }
    }
}