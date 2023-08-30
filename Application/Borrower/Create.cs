using System.Globalization;
using Application.DataStructures;
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
        public class CommandValidtor : AbstractValidator<LoanTypes>
        {
            public CommandValidtor()
            {  //validations for BorrowersDetails Table
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.ContactNumber).NotEmpty().Length(10).WithMessage("Phone number must be 10 digits ");
                RuleFor(x => x.MailingAddress).NotEmpty();
                RuleFor(x => x.Zipcode).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Occupation).NotEmpty();

                //validations for LoanDetails Table
                RuleFor(x => x.PIPmtAmt).NotEmpty().PrecisionScale(9, 3, true);
                RuleFor(x => x.UPBAmt).NotEmpty().PrecisionScale(9, 3, true);
                RuleFor(x => x.RemainingPayments).NotEmpty();
                RuleFor(x => x.PmtDueDate).NotEmpty();



                //validations for LoanInformation Table
                RuleFor(x => x.PropertyAddress).NotEmpty();
                RuleFor(x => x.PriorServicerLoanId).NotEmpty();
                RuleFor(x => x.NoteDate).NotEmpty();
                RuleFor(x => x.LoanBoardingDate).NotEmpty();
                RuleFor(x => x.NoteRatePercent).NotEmpty();
                RuleFor(x => x.Escrow);
                RuleFor(x => x.TaxInsurancePmtAmt).PrecisionScale(9, 2, true);
                RuleFor(x => x.TotalLoanAmount).NotEmpty().PrecisionScale(9, 2, true);
                RuleFor(x => x.LoanTerm).NotEmpty();
                RuleFor(x => x.LoanType).NotEmpty();

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
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                };
                var validator = new CommandValidtor();
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
                    var borrowerRecords = csv.GetRecords<LoanTypes>();
                    var borrowers = new List<BorrowerDetails> { };
                    var data = borrowerRecords.ToArray();

                    for (var i = 0; i < data.Length; i++)
                    {

                        if (data[i] != null)
                        {

                            var validateResult = validator.Validate(data[i]);
                            if (!validateResult.IsValid)
                            {
                                string validationErrors = string.Join(", ", validateResult.Errors.Select(error => error.ErrorMessage));
                                throw new Exception("Validation errors: " + validationErrors);
                            }
                            else
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
                                var validateResult = validator.Validate(data[i]);
                                if (!validateResult.IsValid)
                                {
                                    string validationErrors = string.Join(", ", validateResult.Errors.Select(error => error.ErrorMessage));
                                    _logger.LogError("Validation errors: " + validationErrors);
                                }
                                else
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
                                        NoteDate = data[i].NoteDate,
                                        LoanBoardingDate = data[i].LoanBoardingDate,
                                        BorrowerId = borrowerDetails.BorrowerId,
                                        PriorServicerLoanId = data[i].PriorServicerLoanId,
                                        PropertyAddress = data[i].PropertyAddress
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
                                var validateResult = validator.Validate(data[i]);
                                if (!validateResult.IsValid)
                                {
                                    string validationErrors = string.Join(", ", validateResult.Errors.Select(error => error.ErrorMessage));
                                    _logger.LogError("Validation errors: " + validationErrors);
                                }
                                else
                                {
                                    loanDetails.Add(new LoanDetails
                                    {
                                        PIPmtAmt = data[i].PIPmtAmt,
                                        UPBAmt = data[i].UPBAmt,
                                        RemainingPayments = data[i].RemainingPayments,
                                        PmtDueDate = data[i].PmtDueDate,
                                        LoanInformationId = loanInfo.LoanInformationId,
                                    });
                                    count -= 1;
                                }
                            }
                        }
                    }
                    _context.AddRange(loanDetails);
                    _context.SaveChanges();


    
                    // return Task.FromResult(Unit.Value);


                    var db = from information in _context.LoanInformation
                             join loan in _context.LoanDetails
                             on information.LoanInformationId equals loan.LoanInformationId
                             select new Payment_Schedule
                             {
                                 Escrow_Amount = information.TaxInsurancePmtAmt,
                                 Due_Date = loan.PmtDueDate,
                                 UPB_Amount = loan.UPBAmt,
                                 TotalLoanAmount = information.TotalLoanAmount,
                                 Note_Interest_Rate = information.NoteRatePercent,
                                 Tenure = information.LoanTerm,
                                 Frequency = information.PaymentFreq,
                                 Loan_Id = loan.LoanId,
                                 Escrow = information.Escrow,
                                 RemainingPayments=loan.RemainingPayments
                             };

                    var paymentList = new List<Payment_Schedule>();
                    // var escrowBeneficiaryList = new List<Escrow_Beneficiary>();
                    // var escrowList = new List<Escrow_Disbursement_Schedule>();

                    foreach (var item in db)
                    {
                        int n = 12;
                        if (item.Frequency.Equals("Weekly")) n = 52;
                        else if (item.Frequency.Equals("BiWeekly")) n = 26;

                        decimal r = item.Note_Interest_Rate / n / 100;
                        decimal P = item.TotalLoanAmount * r;
                        decimal num = 1 + r;
                        decimal t = item.Tenure * n;
                        decimal f = (decimal)Math.Pow((double)num, (double)t);
                        decimal d = f - 1;
                        decimal UPB_Amount = item.UPB_Amount;
                        int local=item.RemainingPayments;

                        for (int i = 0; i < 12; i++)
                        {
                            var payment = addPayment(P, f, d, UPB_Amount, r, item.Due_Date, i);
                            payment.Loan_Id = item.Loan_Id;
                            payment.Frequency = item.Frequency;
                            payment.Tenure = item.Tenure;
                            payment.TotalLoanAmount = item.TotalLoanAmount;
                            payment.Note_Interest_Rate = item.Note_Interest_Rate;
                            payment.Escrow = item.Escrow;
                            payment.Escrow_Amount = item.Escrow_Amount;
                            payment.RemainingPayments=local--;

                            if(!item.Escrow) {
                                payment.Escrow_Amount = 0;
                            }
                            
                            paymentList.Add(payment);

                            // var escrowDisbursementSchedule = new Escrow_Disbursement_Schedule();
                            // escrowDisbursementSchedule.date = item.Due_Date.AddMonths(i+1);
                            // escrowDisbursementSchedule.Disbursement_Frequency = 
                            // escrowDisbursementSchedule.Incoming_Escrow = payment.Tax_Amount + payment.Insurance_Amount;
                            UPB_Amount = payment.UPB_Amount;
                        }
                    }

                    await _context.AddRangeAsync(paymentList);
                    await _context.SaveChangesAsync();

                    return await Task.FromResult(Unit.Value);

                }

            }
            public Payment_Schedule addPayment(decimal p, decimal f, decimal d, decimal UPB, decimal r, DateOnly date, int num)
            {
                var item = new Payment_Schedule();
                item.Monthly_Payment_Amount = p * f / d;
                item.Interest_Amount = UPB * r;
                item.Principal_Amount = item.Monthly_Payment_Amount - item.Interest_Amount;
                item.UPB_Amount = UPB - item.Principal_Amount;
                item.Due_Date = date.AddMonths(num + 1);

                return item;
            }
        }
    }
}