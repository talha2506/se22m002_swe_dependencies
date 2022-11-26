namespace customersRestAPI.Entities
{
    public class FinancialProduct
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public int Balance { get; set; }

        public string? ProductCode { get; set; }

        public int InterestRate { get; set; }
    }
}
