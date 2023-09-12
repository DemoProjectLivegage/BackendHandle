using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace Application.GL
{
    public class AllTransaction
    {
        public class Command : IRequest
        {
            public Transactions transaction { get; set; }
        }
        public class Handler : IRequestHandler<Command,Unit>
        {
            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var instance = await _context.Transaction.FindAsync(request.transaction.Id);

                if (instance == null)
                {
                    return Unit.Value; 
                }

                instance.to_account = request.transaction.to_account;
                instance.from_account = request.transaction.from_account;

                _context.Transaction.UpdateRange(instance);
                 _context.SaveChanges();
                return Unit.Value;
            }

        }
    }
}