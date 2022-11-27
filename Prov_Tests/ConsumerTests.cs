using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Provider.Tests
{
    public class ConsumerTests
    {
        private readonly Uri BaseUri = new("http://localhost:5185");
        const string ProviderName = "CustomerAPI";
        const string Consumer1Name = "Consumer1";
        const string Consumer2Name = "Consumer2";

        private ITestOutputHelper _outputHelper { get; }

        public ConsumerTests(ITestOutputHelper output)
        {
            _outputHelper = output;
        }

        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer1()
        {
            var pactFile = new FileInfo(Path.Join("..", "..", "..", "..", "pacts", $"{Consumer1Name}-{ProviderName}.json"));

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_outputHelper)
                },
                LogLevel = PactNet.PactLogLevel.Debug
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider(ProviderName, BaseUri)
                .WithFileSource(pactFile)
                .Verify();
        }

        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer2()
        {
            var pactFile = new FileInfo(Path.Join("..", "..", "..", "..", "pacts", $"{Consumer2Name}-{ProviderName}.json"));

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_outputHelper)
                }
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider(ProviderName, BaseUri)
                .WithFileSource(pactFile)
                .Verify();
        }
    }
}
