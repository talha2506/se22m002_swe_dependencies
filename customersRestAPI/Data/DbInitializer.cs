using customersRestAPI.Entities;

namespace customersRestAPI.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(CustomerContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Customers.Any()) return;

            dbContext.Customers.AddRange(SampleData.customers);
            dbContext.SaveChanges();
        }
    }
}
