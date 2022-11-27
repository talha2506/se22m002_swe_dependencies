using System;

namespace Consumer2
{
    public class Program
    {
        static void main(string[] args)
        {
            var customerClient = new CustomerClient(new Uri("http://localhost:7185"));

            Console.WriteLine("**Retrieving customers with aggregated balance");
            var response = customerClient.GetCustomersWithSumOfBalance().GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine($"Response.Code={response.StatusCode}, Response.Body={responseBody}");
        }
    }
}
