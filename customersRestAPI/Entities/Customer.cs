using customersRestAPI.Data;

namespace customersRestAPI.Entities
{
    public class Customer
    {
        public long Id { get; set; }

        public string? Name { get; set; }
        
        public string? Address { get; set; }

        public DateTime Birthdate { get; set; }

        public string? EmailAddress { get; set; }

        public string? Status { get; set; }

        public List<FinancialProduct> FinancialProducts = SampleData.financialProducts.ToList();
    }
}