using Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace Application.GL
{
    public class AllTransaction
    {
        public class Command : IRequest<Transactions>
        {
            public Transactions transaction { get; set; }
        }
        public class Handler : IRequestHandler<Command, Transactions>
        {
            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<Transactions> Handle(Command request, CancellationToken cancellationToken)
            {
                var instance = _context.Transaction.Find(request.transaction.Id);
                instance.to = request.transaction.to;
                instance.from = request.transaction.from;
                _context.Transaction.Update(instance);
                _context.SaveChanges();
                return instance;
            }
        }
    }
}