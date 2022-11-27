using System;

namespace Consumer1
{
    public class Program
    {
        static void main(string[] args)
        {
            var customerClient = new CustomerClient(new Uri("http://localhost:7185"));

            Console.WriteLine("**Retrieving customers with basic informations");
            var response = customerClient.GetCustomersWithBasicInfos().GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine($"Response.Code={response.StatusCode}, Response.Body={responseBody}");
        }
    }
}
