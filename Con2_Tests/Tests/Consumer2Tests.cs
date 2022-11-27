using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Matchers;
using Xunit;
using Xunit.Abstractions;

namespace Consumer2.Tests
{
    public class Consumer2Tests
    {
        private IPactBuilderV3 pact;
        private readonly CustomerClient customerClient;
        private readonly int port = 9000;
        private readonly List<object> customers;

        public Consumer2Tests(ITestOutputHelper output)
        {
            this.customers = new List<object>()
            {
                new { name = "Max Mustermann", aggregatedBalance = 89300 },
                new { name = "Maxime Musterfrau", aggregatedBalance = 89300 },
                new { name = "Max Mustervater", aggregatedBalance = 89300 },
                new { name = "Maxime Mustermama", aggregatedBalance = 89300 },
                new { name = "Max Musteropa", aggregatedBalance = 89300 }
            };

            var Config = new PactConfig
            {
                PactDir = Path.Join("..", "..", "..", "..", "pacts"),

                Outputters = new[] { new XUnitOutput(output) },
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            this.pact = Pact.V3("Consumer2", "CustomerAPI", Config).UsingNativeBackend(port);
            this.customerClient = new CustomerClient(new Uri($"http://localhost:{port}"));
        }

        [Fact]
        public async void GetCustomersWithBasicInfos()
        {
            pact.UponReceiving("A valid request for all customers with their aggregated balance")
                    .Given("customers exist")
                    .WithRequest(HttpMethod.Get, "/api/Customers/withSumOfBalance")
                .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithJsonBody(new TypeMatcher(customers));

            await pact.VerifyAsync(async ctx => {
                var response = await customerClient.GetCustomersWithSumOfBalance();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }
    }
}
