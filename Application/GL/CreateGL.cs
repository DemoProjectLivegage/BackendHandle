using Domain;
using MediatR;
using Persistence;

namespace Application.GL
{
    public class CreateGL 
    {
        public class Command : IRequest<GeneralLedger>
        {
            public GeneralLedger data { get; set; }
        }

        public class Handler : IRequestHandler<Command, GeneralLedger>
        {
            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;

            }
            public async Task<GeneralLedger> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await _context.GeneralLedger.AddAsync(request.data);
                await _context.SaveChangesAsync(); 
                return response.Entity;
            }
        }

    }
}