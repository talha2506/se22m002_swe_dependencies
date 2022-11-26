namespace customersRestAPI.DTOs
{
    public class CustomerWithSumOfBalanceDTO
    {
        public string? Name { get; set; }

        public double AggregatedBalance { get; set; }
    }
}
