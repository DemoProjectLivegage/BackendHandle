using Domain;
using MediatR;
using Persistence;


namespace Application.Escrow_schedule
{
    public class Schedule
    {
        public class Command : IRequest
        {
            public int LoanInformationId { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DatabaseContext _context;
            // private readonly ILogger _logger;
            public Handler(DatabaseContext context)
            {
                // _logger = logger;
                _context = context;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var db = from information in _context.LoanInformation
                         join loan in _context.LoanDetails
                         on information.LoanInformationId equals loan.LoanInformationId
                         select new PaymentSchedule
                         {
                             Tax_Amount = information.TaxInsurancePmtAmt,
                             Insurance_Amount = information.TaxInsurancePmtAmt,
                             Due_Date = loan.PmtDueDate,
                             UPB_Amount = loan.UPBAmt,
                             TotalLoanAmount = information.TotalLoanAmount,
                             Note_Interest_Rate = information.NoteRatePercent,
                             Tenure = information.LoanTerm,
                             Frequency = information.PaymentFreq,
                             Id = information.LoanInformationId
                         };
                var paymentList = new List<PaymentSchedule>();

                foreach (var item in db)
                {
                    // Console.WriteLine("\n\n\n"+item+"\n\n\n");
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

                    for (int i = 0; i < 12; i++)
                    {
                        var payment = addPayment(P, f, d, UPB_Amount, r, item.Due_Date, i);
                        payment.Id=item.Id;
                        payment.Frequency=item.Frequency;
                        payment.Tenure=item.Tenure;
                        payment.TotalLoanAmount=item.TotalLoanAmount;
                        payment.Note_Interest_Rate=item.Note_Interest_Rate;

                        decimal Property_Tax = (decimal)1.2*item.TotalLoanAmount/100;
                        decimal Home_Insurance=1500;
                        decimal PMI=(decimal)0.5*item.TotalLoanAmount/100;
                        decimal flood_Insurance=739/n;

                        payment.Tax_Amount = Property_Tax/12;
                        payment.Insurance_Amount= (Home_Insurance + PMI   + flood_Insurance)/12;
                        paymentList.Add(payment);
                        UPB_Amount=payment.UPB_Amount;
                    }
                    await _context.AddRangeAsync(paymentList);
                    await _context.SaveChangesAsync();


                }   
            // await  _context.PaymentSchedule.FindAsync(request.LoanInformationId);
            return Unit.Value;
            }
            public PaymentSchedule addPayment(decimal p, decimal f, decimal d, decimal UPB, decimal r, DateOnly date, int num)
            {
                var item = new PaymentSchedule();
                item.Monthly_Payment_Amount = p * f / d;
                item.Interest_Amount = UPB * r;
                item.Principal_Amount = item.Monthly_Payment_Amount - item.Interest_Amount;
                item.UPB_Amount = UPB - item.Principal_Amount;
                item.Due_Date = date.AddMonths(num + 1);

                return item;

            }

            // Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            // {
            //     throw new NotImplementedException();
            // }
        }
    }
}