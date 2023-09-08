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
                        threshold = 50,
                        date = DateOnly.FromDateTime(DateTime.Now),
                        // UPB_Amount = waterfall.UPB_Amount

                    };
                    decimal remaining = request.incoming_amount;//300

                    if (waterfall.Monthly_Payment_Amount - request.incoming_amount > modify.threshold)
                    {
                        modify.suspence = request.incoming_amount;
                        remaining = 0;
                        modify.UPB_Amount = waterfall.UPB_Amount + waterfall.Principal_Amount;
                        await _context.Payment_Hierarchy.AddAsync(modify);
                        await _context.SaveChangesAsync();
                        return Unit.Value;
                    }



                    if (waterfall.Interest_Amount <= remaining)
                    {
                        modify.interest = waterfall.Interest_Amount;//54
                        remaining = remaining - waterfall.Interest_Amount;//300-54
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
                        modify.UPB_Amount = waterfall.UPB_Amount;
                    }
                    else
                    {
                        modify.principal = remaining;
                        modify.UPB_Amount = waterfall.UPB_Amount + (modify.principal - remaining);
                        remaining = 0;
                    }
                    if (waterfall.Escrow)
                    {
                        if (waterfall.Escrow_Amount <= remaining)
                        {
                            modify.escrow = waterfall.Escrow_Amount;
                            remaining = remaining - waterfall.Escrow_Amount;
                            if (remaining > 0)
                            {
                                if(request.id==3)
                                {
                                    if (remaining <= 10)
                                {
                                    modify.late_charge = remaining;

                                }
                                else
                                {
                                    modify.late_charge = 10;
                                    modify.suspence = remaining - 10;
                                }
                                }
                                else{
                                   modify.suspence=remaining; 
                                }
                            }
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
                        if (remaining > 0)
                        {
                            if(request.id==3)
                            {
                                  if (remaining <= 10)
                            {
                                modify.late_charge = remaining;

                            }
                            else
                            {
                                modify.late_charge = 10;
                                modify.suspence = remaining - 10;
                            }
                            }
                            else{
                                modify.suspence=remaining;
                            }
                          
                        }
                    }
                    // if (waterfall.Principal_Amount <= remaining)
                    // {
                    //     modify.principal = waterfall.Principal_Amount;//147
                    //     remaining = remaining - waterfall.Principal_Amount;//246-147=99
                    //     modify.UPB_Amount = waterfall.UPB_Amount;
                    // }
                    // else
                    // {
                    //     modify.principal = remaining;
                    //     modify.UPB_Amount = waterfall.UPB_Amount + (modify.principal - remaining);
                    //     remaining = 0;
                    // }


                    _context.AddRange(modify);
                    _context.SaveChanges();

                    var ld = await _context.LoanDetails.FindAsync(request.id);
                    var new_id = waterfall.Id + 1;

                    Payment_Schedule ps = await _context.Payment_Schedule.FindAsync(new_id);
                    ld.PIPmtAmt = ps.Principal_Amount + ps.Interest_Amount;
                    ld.UPBAmt = ps.UPB_Amount + ps.Principal_Amount; // This is done for showing current month UPB amount.
                    ld.RemainingPayments = ps.RemainingPayments;
                    ld.PmtDueDate = ps.Due_Date;

                    _context.LoanDetails.UpdateRange(ld);
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