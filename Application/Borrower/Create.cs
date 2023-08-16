using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
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

            // private List<EntityEntry> _obj = new List<EntityEntry>();

            public Handler(DatabaseContext context, ILogger<Create> logger)
            {
                _context = context;
                _logger = logger;
            }
            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                };

                using (var reader = new StreamReader(@"C:\Users\IMehta\Downloads\Loan_data.csv"))
                using (var csv = new CsvReader(reader, configuration))
                {
                    /*
                    * First I have stored the data of borrower details. So, that i could get the 
                    * Borrower details Id from changes and then use them further.
                    * For creating object normal for loop has been used because foreach loop change the value of current 
                    * pointer and make the object unusable.
                    */
                    var borrowerRecords = csv.GetRecords<BorrowerTypes>();
                    var borrowers = new List<BorrowerDetails> { };
                    var data = borrowerRecords.ToArray();

                    for (var i = 0; i < data.Length; i++)
                    {
                        if (data[i] != null)
                        {
                            borrowers.Add(new BorrowerDetails
                            {
                                FullName = data[i].FullName,
                                ContactNumber = data[i].ContactNumber,
                                MailingAddress = data[i].MailingAddress,
                                Email = data[i].Email,
                                Occupation = data[i].Occupation,
                                Zipcode = data[i].Zipcode

                            });
                        }
                    }
                    _context.AddRange(borrowers);
                    _context.SaveChanges();
                    var obj = _context.ChangeTracker.Entries().ToArray();
                    //Here I'm saving the data into loan information table 
                    var loanInformation = new List<LoanInformation> { };
                    var count = 0;
                    for (var i = 0; i < data.Length; i++)
                    {
                        if (obj[count].Entity is BorrowerDetails borrowerDetails)
                        {
                            if (data[i] != null)
                            {
                                loanInformation.Add(new LoanInformation
                                {
                                    NoteRatePercent = Convert.ToDecimal(data[i].NoteRatePercent),
                                    Escrow = data[i].Escrow,
                                    TaxInsurancePmtAmt = data[i].TaxInsurancePmtAmt,
                                    TotalLoanAmount = data[i].TotalLoanAmount,
                                    LoanTerm = data[i].LoanTerm,
                                    LoanType = data[i].LoanType,
                                    PaymentFreq = data[i].PaymentFreq,
                                    PrimaryContact = data[i].PrimaryContact,
                                    BorrowerId = borrowerDetails.BorrowerId,

                                });
                                count += 1;
                            }
                        }
                    }
                    _context.AddRange(loanInformation);
                    _context.SaveChanges();
                    obj = _context.ChangeTracker.Entries().ToArray();
                    
                 // _logger.LogInformation("values of obj \n \n \n "+obj[count]+"\n\n\n");
                    var loanDetails = new List<LoanDetails> { };
                    //count = 0; // Re set the value of count so that we can use it again

                    for (var i = 0; i < data.Length; i++)
                    {
                       //var infos= loanInformation.ToArray();

                        if (obj[count].Entity is LoanInformation loanInfo)
                        {
                            if (data[i] != null)
                            {
                                loanDetails.Add(new LoanDetails
                                {
                                    PIPmtAmt = data[i].PIPmtAmt,
                                    UPBAmt = data[i].UPBAmt,
                                    RemainingPayments = data[i].RemainingPayments,
                                    PmtDueDate = data[i].PmtDueDate,
                                    PropertyAddress = data[i].PropertyAddress,
                                    LoanInformationId = loanInfo.LoanInformationId,
                                });
                                 count+=1; 
                            }
                            
                        }
                      
                    }
                  


                    _context.AddRange(loanDetails);
                    _context.SaveChanges();

                }
                return Task.FromResult(Unit.Value);
            }
        }
    }

    class BorrowerTypes

    {
        //Borrower Details Table
        public string FullName { set; get; }
        public string ContactNumber { set; get; }
        public string MailingAddress { set; get; }
        public int Zipcode { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        //Loan Details Table
      
        public decimal PIPmtAmt { get; set; }
        public decimal UPBAmt { get; set; }
        public decimal RemainingPayments { get; set; }
        public DateOnly PmtDueDate { get; set; }
        public required string PropertyAddress { get; set; }


        //Loan Information Table

        public int PriorServicerLoanId { get; set; }

        public DateOnly NoteDate { get; set; }
        public DateOnly LoanBoardingDate { get; set; }

        public decimal NoteRatePercent { get; set; }

        public bool Escrow { get; set; }

        public decimal TaxInsurancePmtAmt { get; set; }

        public decimal TotalLoanAmount { get; set; }

        public int LoanTerm { get; set; }

        public string LoanType { get; set; }


        public string PaymentFreq { get; set; }

        public string PrimaryContact { get; set; }
    }
}