using Application.DTO;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.GL
{
    public class ShowAllGL
    {
        public class Query : IRequest<List<COA_DTO>>
        {
            public int Id { get; set; } = 0;

        }

        public class Handler : IRequestHandler<Query, List<COA_DTO>>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;

            public Handler(DatabaseContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<List<COA_DTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Payment_Hierarchy> incoming_payments;
                if (request.Id != 0)
                {
                    incoming_payments = _context.Payment_Hierarchy.Where(x => x.Loan_id == request.Id).ToList();
                }
                else
                {
                    incoming_payments = _context.Payment_Hierarchy.ToList();
                }

                List<COA_DTO> COA_List = _mapper.Map<List<COA_DTO>>(await _context.COA.Include(b => b.gl_list).ToListAsync());
                List<Transactions> all_gl = _context.Transaction.ToList();

                // Here we are checking weather the transaction mapping is done or not. If not then it will return null.
                if (all_gl[0].from_account == null) return null;

                if (!incoming_payments.Any())
                {
                    return null;
                }
                //    Payment_Hierarchy item ;
                COA_List = COA_List.Select(lis =>
                {
                    lis.gl_list = lis.gl_list.Select(x =>
                    {
                        foreach (var item in incoming_payments)
                        {
                            if (x.description.ToLower().Contains("interest") && item.interest != 0)
                            {
                                x.value += item.interest;
                            }
                            if (x.description.ToLower().Contains("month") && item.Monthly_Payment_Amount != 0)
                            {
                                x.value += item.Monthly_Payment_Amount;
                            }
                            if (x.description.ToLower().Contains("principal") && item.principal != 0)
                            {
                                x.value += item.principal;
                            }
                            if (x.description.ToLower().Contains("escrow") && item.escrow != 0)
                            {
                                x.value += item.escrow;
                            }
                            if (x.description.ToLower().Contains("late") && item.late_charge != 0)
                            {
                                x.value += item.late_charge;
                            }
                            if (x.description.ToLower().Contains("other") && item.other_fee != 0)
                            {
                                x.value += item.other_fee;
                            }
                            if (x.description.ToLower().Contains("suspence") && item.suspence != 0)
                            {
                                x.value += item.suspence;
                            }
                        }
                        return x;
                    }).ToList();

                    return lis;
                }).ToList();
                return COA_List;
            }
        }
    }
}