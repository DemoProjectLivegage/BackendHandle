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
         public class BorrowerDetailsMap : ClassMap<BorrowerDetails>
        {
            public BorrowerDetailsMap()
            {
                 Map(m => m.FullName).Name("FullName");
                Map(m => m.ContactNumber).Name("ContactNumber");
                Map(m => m.MailingAddress).Name("MailingAddress");
                Map(m=>m.Zipcode).Name("ZipCode");
                Map(m=>m.Email).Name("Email");
                Map(m=>m.Occupation).Name("Occupation");

                
                Map(m => m.BorrowerId).Ignore();
            }
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
                    IgnoreBlankLines = true,
                     PrepareHeaderForMatch = args => args.Header.ToLower(),
                    
                };
                
        
              using (var reader = new StreamReader(@"C:\Users\IMehta\Downloads\Loan_data.csv"))
            using (var csv = new CsvReader(reader, configuration))
            {
               csv.Context.RegisterClassMap<BorrowerDetailsMap>();
                var records = csv.GetRecords<BorrowerDetails>(); 
                
                foreach (var record in records)
                { 
                    
                      _context.BorrowersDetails.Add(record);   
                   _logger.LogInformation(Convert.ToString(record));
                
                }

                await _context.SaveChangesAsync();
            }
            return Unit.Value;
            }
        }

    }
}
