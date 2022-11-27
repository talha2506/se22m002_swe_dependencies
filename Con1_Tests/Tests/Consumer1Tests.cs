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

namespace Consumer1.Tests
{
    public class Consumer1Tests
    {
        private IPactBuilderV3 pact;
        private readonly CustomerClient customerClient;
        private readonly int port = 9000;
        private readonly List<object> customers;

        public Consumer1Tests(ITestOutputHelper output)
        {
            this.customers = new List<object>()
            {
                new { name = "Max Mustermann", emailAddress = "max.mustermann@gmx.at", status = "active" },
                new { name = "Maxime Musterfrau", emailAddress = "maxime.musterfrau@gmx.at", status = "active" },
                new { name = "Max Mustervater", emailAddress = "max.mustervater@gmx.at", status = "active" },
                new { name = "Maxime Mustermama", emailAddress = "maxime.mustermama@gmx.at", status = "active" },
                new { name = "Max Musteropa", emailAddress = "max.musteropa@gmx.at", status = "active" }
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

            this.pact = Pact.V3("Consumer1", "CustomerAPI", Config).UsingNativeBackend(port);
            this.customerClient = new CustomerClient(new Uri($"http://localhost:{port}"));
        }

        [Fact]
        public async void GetCustomersWithBasicInfos()
        {
            pact.UponReceiving("A valid request for all customers with basic infos")
                    .Given("customers exist")
                    .WithRequest(HttpMethod.Get, "/api/Customers/basicInfo")
                .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithJsonBody(new TypeMatcher(customers));

            await pact.VerifyAsync(async ctx => {
                var response = await customerClient.GetCustomersWithBasicInfos();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }
    }
}
