using Application.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.GL
{
    public class ShowAllGL
    {
        public class Query : IRequest<List<COA_DTO>>
        { }

        public class Handler : IRequestHandler<Query, List<COA_DTO>>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;

            public Handler(DatabaseContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<List<COA_DTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var incoming_payments = _context.Payment_Hierarchy.ToList();
                List<COA_DTO> COA_List = _mapper.Map<List<COA_DTO>>(await _context.COA.Include(b => b.gl_list).ToListAsync());
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
                            if (x.description.ToLower().Contains("interest") && item.interest !=0)
                            {
                                x.value += item.interest;
                            }
                            if (x.description.ToLower().Contains("principal") && item.principal !=0)
                            {
                                x.value += item.principal;
                            }
                            if (x.description.ToLower().Contains("escrow") && item.escrow !=0)
                            {
                                x.value += item.escrow;
                            }
                            if (x.description.ToLower().Contains("late") && item.late_charge !=0)
                            {
                                x.value += item.late_charge;
                            }
                            if (x.description.ToLower().Contains("other") && item.other_fee !=0)
                            {
                                x.value += item.other_fee;
                            }
                            if (x.description.ToLower().Contains("suspence") && item.suspence !=0)
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