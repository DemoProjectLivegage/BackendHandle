using Application.Enums;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Escrow_schedule
{
    public class Escrow_Benificiary
    {
        public class Command : IRequest
        {
            public Benificiary benificiary { get; set; }
        }

        public class CommandValidtor : AbstractValidator<Benificiary>
        {
            public CommandValidtor()
            {
                RuleFor(x => x.escrow_type).NotEmpty();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.account_no).NotEmpty().Length(12);
                RuleFor(x => x.routing_no).NotEmpty().Length(6);
                RuleFor(x => x.payment_mode).NotEmpty().Must(mode=> Enum.TryParse(typeof(PaymentModes), mode, out _)).WithMessage("Invalid payment mode.");
                RuleFor(x => x.frequency).NotEmpty().Must(mode=> Enum.TryParse(typeof(Bene_Frequency), mode, out _)).WithMessage("Invalid Frequency.");;
            }

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
                
                await _context.Benificiary.AddAsync(request.benificiary);
                await _context.SaveChangesAsync();   
                return Unit.Value;
            }
        }
       
    }
}