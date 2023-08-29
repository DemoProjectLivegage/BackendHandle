using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Payment_Hierarchy_
{
    public class Payment_Hierarchy_Application
    {
        public class Command : IRequest
        {
            public int id { get; set; }
            public DateOnly payment_date { get; set; }
            public decimal incoming_amount { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    Payment_Schedule waterfall = await _context.Payment_Schedule.Where(x => x.Loan_Id == request.id
                    && x.Due_Date == request.payment_date).FirstOrDefaultAsync();



                    Payment_Hierarchy modify = new Payment_Hierarchy()
                    {
                        Loan_id = waterfall.Loan_Id,
                        Monthly_Payment_Amount = waterfall.Monthly_Payment_Amount,
                        actual_receive = request.incoming_amount,
                        threshold = 50
                    };
                    decimal remaining = request.incoming_amount;//300

                    if (waterfall.Monthly_Payment_Amount - request.incoming_amount > modify.threshold)
                    {
                        modify.suspence = request.incoming_amount;
                        remaining = 0;
                    }



                    if (waterfall.Interest_Amount <= remaining)
                    {
                        modify.interest = waterfall.Interest_Amount;//54
                        remaining = remaining - waterfall.Interest_Amount;//300-54

                        if (request.id == 3)
                        {
                            modify.late_charge = 10;
                            remaining = remaining - 10;
                        }
                    }

                    else
                    {
                        modify.interest = remaining;
                        remaining = 0;

                    }


                    if (waterfall.Principal_Amount <= remaining)
                    {
                        modify.principal = waterfall.Principal_Amount;//147
                        remaining = remaining - waterfall.Principal_Amount;//246-147=99
                    }
                    else
                    {
                        modify.principal = remaining;
                        remaining = 0;
                    }

                    if (waterfall.Escrow)
                    {
                        if (waterfall.Escrow_Amount <= remaining)
                        {
                            modify.escrow = waterfall.Escrow_Amount;
                            remaining = remaining - waterfall.Escrow_Amount;
                            modify.suspence = remaining;
                            remaining = 0;
                        }
                        else
                        {
                            modify.escrow = remaining;
                            remaining = 0;
                        }
                    }
                    else
                    {
                        remaining = remaining - waterfall.Escrow_Amount;
                        modify.suspence = remaining;
                        remaining = 0;
                    }
                    _context.AddRange(modify);
                    _context.SaveChanges();
                    return Unit.Value;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}