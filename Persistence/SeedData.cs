using Domain;

namespace Persistence
{
    public class SeedData
    {
        public static async Task CreateTransactionData(DatabaseContext context)
        {
            if (context.Transaction.Any()) return;
            List<Transactions> transactions = new List<Transactions>
            {
                new Transactions{
                    Id=1,
                    transaction_name="Principal Collected",

                },
                new Transactions{
                    Id=2,
                    transaction_name="Interest Collected",

                },
                new Transactions{
                    Id=3,
                    transaction_name="Escrow Collected",

                },
                new Transactions{
                    Id=4,
                    transaction_name="Late Charge Collected",

                },
                new Transactions{
                    Id=5,
                    transaction_name="Late Charge Assessed",

                },
                new Transactions{
                    Id=6,
                    transaction_name="Suspence",

                },
            };
            await context.Transaction.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }

    }
}