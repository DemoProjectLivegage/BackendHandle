using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;
namespace Application.Borrower
{
    public class Create
    {
        public class Command : IRequest
        {
          
        }
        public class Handler : IRequestHandler<Command>
        {  
              private readonly DatabaseContext _context;
              private readonly ILogger<Create> _logger;

        public Handler(DatabaseContext context, ILogger<Create> logger)
        {
            _context = context;
            _logger=logger;
        }
            public  async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture){
                    
                };
 
              using (var reader = new StreamReader(@"C:\Users\IMehta\Downloads\Loan_data.csv"))
            using (var csv = new CsvReader(reader, configuration))
            {
                var records = csv.GetRecords<BorrowerDetails>(); 
             
                foreach (var record in records)
                { if(record!=null)
                {    
                      
                     _context.BorrowersDetails.Add(record);

                   
                   _logger.LogInformation(Convert.ToString(record));
                }
                }

                await _context.SaveChangesAsync();
            }
            return Unit.Value;
            }
        }

    }
}
