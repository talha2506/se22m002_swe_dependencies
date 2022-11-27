using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer2
{
    public class CustomerClient
    {
        private readonly Uri BaseUri;

        public CustomerClient(Uri baseUri)
        {
            this.BaseUri = baseUri;
        }

        public async Task<HttpResponseMessage> GetCustomersWithSumOfBalance()
        {
            using (var client = new HttpClient { BaseAddress = BaseUri })
            {
                try
                {
                    var response = await client.GetAsync($"/api/Customers/withSumOfBalance");
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem connecting to Provider API.", ex);
                }
            }
        }
    }
}
