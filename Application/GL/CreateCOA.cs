using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Azure;
using Domain;
using MediatR;
using Persistence;

namespace Application.GL
{
    public class CreateCOA
    {
        public class Command : IRequest<COA_DTO>
        {
            public COA coa {get;set;}
        }
        public class Handler : IRequestHandler<Command, COA_DTO>
        {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
            public Handler (DatabaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<COA_DTO> Handle(Command request, CancellationToken cancellationToken)
            {
               var response = await _context.COA.AddAsync(request.coa);
                await _context.SaveChangesAsync();
                var result = _mapper.Map<COA_DTO>(response.Entity);
                return result;
            }
        }
    }
}