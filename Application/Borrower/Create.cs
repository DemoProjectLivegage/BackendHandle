            using System.ComponentModel.DataAnnotations;
            using System.Globalization;
            using System.Linq.Expressions;
            using CsvHelper;
            using CsvHelper.Configuration;
            using Domain;
            using FluentValidation;
            using MediatR;
            using Microsoft.Extensions.Logging;
            using Persistence;


            namespace Application.Borrower
            {
                public class Create
                {
                    public class Command : IRequest
                    {
                        public string file { get; set; }
                    }

                    //Validation Function 
                public class CommandValidtor : AbstractValidator<BorrowerTypes>
                {
                public CommandValidtor()
                {  //validations for BorrowersDetails Table
                    RuleFor(x=>x.FullName).NotEmpty();
                    RuleFor(x=>x.ContactNumber).NotEmpty().Length(11).WithMessage("Phone number must be 12 digits ");
                    RuleFor(x=>x.MailingAddress).NotEmpty();
                    RuleFor(x=>x.Zipcode).NotEmpty();
                    RuleFor(x=>x.Email).NotEmpty().EmailAddress();
                    RuleFor(x=>x.Occupation).NotEmpty();
                    
                    //validations for LoanDetails Table
                    RuleFor(x=>x.PIPmtAmt).NotEmpty().PrecisionScale(6,2,true);
                    RuleFor(x=>x.UPBAmt).NotEmpty().PrecisionScale(7,2,true);
                    RuleFor(x=>x.RemainingPayments).NotEmpty();
                    RuleFor(x=>x.PmtDueDate).NotEmpty();
                    RuleFor(x=>x.PropertyAddress).NotEmpty();


                    //validations for LoanInformation Table

                    RuleFor(x=>x.PriorServicerLoanId).NotEmpty();
                    RuleFor(x=>x.NoteDate).NotEmpty();
                    RuleFor(x=>x.LoanBoardingDate).NotEmpty();
                    RuleFor(x=>x.NoteRatePercent).NotEmpty();
                    RuleFor(x=>x.Escrow);
                    RuleFor(x=>x.TaxInsurancePmtAmt).PrecisionScale(5,2,true);
                    RuleFor(x=>x.TotalLoanAmount).NotEmpty().PrecisionScale(6,2,true);
                    RuleFor(x=>x.LoanTerm).NotEmpty();
                    RuleFor(x=>x.LoanType).NotEmpty();
                    
                }
                }
                    public class Handler : IRequestHandler<Command>
                    {
                        private readonly DatabaseContext _context;
                        private readonly ILogger<Create> _logger;

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
                            var validator=new CommandValidtor();
                            using (var fileStream = new FileStream(request.file, FileMode.Open))
                            using (var reader = new StreamReader(fileStream))
                            using (var csv = new CsvReader(reader, configuration))
                            {
                            
                                /*
                                * First I have stored the data of borrower details. So, that i could get the 
                                * Borrower details Id from changes and then use them further.
                                * For creating object normal for loop has been used because foreach loop change the value of current 
                                * pointer and make the object unusable.
                                */
                            // try
                            var borrowerRecords = csv.GetRecords<BorrowerTypes>();
                                var borrowers = new List<BorrowerDetails> { };
                                var data = borrowerRecords.ToArray();

                                for (var i = 0; i < data.Length; i++)
                                {
                                
                                    if (data[i] != null)
                                    {

                                        var validateResult=validator.Validate(data[i]);
                                    if(!validateResult.IsValid)
                                    {
                        string validationErrors = string.Join(", ", validateResult.Errors.Select(error => error.ErrorMessage));
                        throw new Exception ("Validation errors: " + validationErrors);
                        }
                                    else{
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
                                        var validateResult=validator.Validate(data[i]);
                                    if(!validateResult.IsValid)
                                    {
                                        string validationErrors = string.Join(", ", validateResult.Errors.Select(error => error.ErrorMessage));
                                        _logger.LogError("Validation errors: " + validationErrors);
                                    }
                                    else{
                                            loanInformation.Add(new LoanInformation
                                            {
                                                NoteRatePercent = Convert.ToDecimal(data[i].NoteRatePercent),
                                                Escrow = data[i].Escrow,
                                                TaxInsurancePmtAmt = data[i].TaxInsurancePmtAmt,
                                                TotalLoanAmount = data[i].TotalLoanAmount,
                                                LoanTerm = data[i].LoanTerm,
                                                LoanType = data[i].LoanType,
                                                PaymentFreq = data[i].PaymentFreq,
                                                NoteDate = data[i].NoteDate,
                                                LoanBoardingDate = data[i].LoanBoardingDate,
                                                BorrowerId = borrowerDetails.BorrowerId,
                                                PriorServicerLoanId = data[i].PriorServicerLoanId,

                                            });
                                            count += 1;
                                        }
                                        }
                                    }
                                }
                                        _context.AddRange(loanInformation);
                                        _context.SaveChanges();
                                        obj = _context.ChangeTracker.Entries().ToArray();
                                        var loanDetails = new List<LoanDetails> { };
                                        count = obj.Length - 1; 
                                        for (var i = 0; i < data.Length; i++)
                                        {

                                            if (obj[count].Entity is LoanInformation loanInfo)
                                            {
                                                if (data[i] != null)
                                                {
                                                    var validateResult=validator.Validate(data[i]);
                                            if(!validateResult.IsValid)
                                            {
                                                string validationErrors = string.Join(", ", validateResult.Errors.Select(error => error.ErrorMessage));
                                                _logger.LogError("Validation errors: " + validationErrors);
                                            }
                                            else{
                                                    loanDetails.Add(new LoanDetails
                                                    {
                                                        PIPmtAmt = data[i].PIPmtAmt,
                                                        UPBAmt = data[i].UPBAmt,
                                                        RemainingPayments = data[i].RemainingPayments,
                                                        PmtDueDate = data[i].PmtDueDate,
                                                        PropertyAddress = data[i].PropertyAddress,
                                                        LoanInformationId = loanInfo.LoanInformationId,
                                                    });
                                                    count -= 1;
                                                }
                                                }
                                            }
                                        }
                                        _context.AddRange(loanDetails);
                                        _context.SaveChanges();

                                
                            // } catch(Exception e)
                            // {
                            //     Console.WriteLine("\n\n\n\n Errorrr check the daata \n\n\n", e);

                            //     }
                                return Task.FromResult(Unit.Value);
                            }
                        
                        }
                    }
                }
            public class BorrowerTypes

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
                    public int RemainingPayments { get; set; }
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
                    // public 
                    // public string PrimaryContact { get; set; }
                }
            }