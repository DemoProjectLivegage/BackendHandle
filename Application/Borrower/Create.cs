using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;
namespace Application.Borrower
{
    public class Create
    {
        public class Command : IRequest
        {
          
        }

        public class Handler : IRequestHandler<Command>
        {  
              private readonly DatabaseContext _context;
              private readonly ILogger<Create> _logger;

        public Handler(DatabaseContext context, ILogger<Create> logger)
        {
            _context = context;
            _logger=logger;
        }
            public  async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture){
                    
                    
                };
                
        
              using (var reader = new StreamReader(@"C:\Users\IMehta\Downloads\Loan_data.csv"))
            using (var csv = new CsvReader(reader, configuration))
            {
              
            var records = csv.GetRecords<Types>();
                    
                       var loaninformation=new List<LoanInformation>{};
                    var borrowers = new List<BorrowerDetails>{};
                    var loans=new List<LoanDetails>{};
                 
                    foreach (var record in records)
                    {
                        if (record != null)

                        {
                             loaninformation.Add(new LoanInformation
                    { // BorrowerId=record.BorrowerId,
                       PriorServicerLoanId=record.PriorServicerLoanId,
                      NoteDate=record.NoteDate,
                      LoanBoardingDate=record.LoanBoardingDate,
                      NoteRatePercent=record.NoteRatePercent,
                      Escrow=record.Escrow,
                      TaxInsurancePmtAmt=record.TaxInsurancePmtAmt,
                      TotalLoanAmount=record.TotalLoanAmount,
                      LoanTerm=record.LoanTerm,
                      LoanType=record.LoanType,
                      PaymentFreq=record.PaymentFreq,
                      PrimaryContact=record.PrimaryContact


                    });
                            borrowers.Add(new BorrowerDetails

                            {
                                FullName = record.FullName,

                                ContactNumber = record.ContactNumber,

                                MailingAddress = record.MailingAddress,

                                Email = record.Email,

                                Occupation = record.Occupation,

                                Zipcode = record.Zipcode

                            }); 
                          
                    loans.Add(new LoanDetails
                    {
                        PIPmtAmt = record.PIPmtAmt,
                        UPBAmt = record.UPBAmt,
                        RemainingPayments = record.RemainingPayments,
                        PmtDueDate = record.PmtDueDate,
                        PropertyAddress = record.PropertyAddress,
                    });
                   

                        }
                    }
                     await _context.LoanInformation.AddRangeAsync(loaninformation);
                     
                      
                    await _context.BorrowersDetails.AddRangeAsync(borrowers);
                    await _context.LoanDetails.AddRangeAsync(loans);
                   
                    await _context.SaveChangesAsync();

                }

                return Unit.Value;

            }

        }

        class Types

        {  
            //Borrower Details Table
            public string FullName { set; get; }

            public string ContactNumber { set; get; }

            public string MailingAddress { set; get; }

            public int Zipcode { get; set; }

            public string Email { get; set; }

            public string Occupation { get; set; }


          //Loan Details Table
          public decimal PIPmtAmt {get; set;}

        public decimal UPBAmt {get; set;}

        public  decimal RemainingPayments {get; set;}

        public DateOnly PmtDueDate {get; set;}

        public required string PropertyAddress {get; set;}


        //Loan Information Table


       //  public int BorrowerId {get; set;}
         public  int PriorServicerLoanId {get; set;}

         public  DateOnly NoteDate {get; set;}
        public  DateOnly LoanBoardingDate{get; set;}

        public decimal NoteRatePercent {get; set;}

        public bool Escrow {get; set;}

        public  decimal TaxInsurancePmtAmt {get; set;}

        public  decimal TotalLoanAmount {get; set;}

        public  int LoanTerm {get; set;}

        public string LoanType {get; set;}

       
         public  string PaymentFreq {get; set;}

       public string PrimaryContact {get;set;}
        }
        
            }
        }

    

