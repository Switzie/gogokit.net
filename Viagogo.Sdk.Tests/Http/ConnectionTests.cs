﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Viagogo.Sdk.Authentication;
using Viagogo.Sdk.Http;
using Viagogo.Sdk.Tests.Fakes;

namespace Viagogo.Sdk.Tests.Http
{
    [TestFixture]
    public class ConnectionTests
    {
        private static Connection CreateConnection(
            ProductHeaderValue productHeader = null,
            ICredentialsProvider credsPrv = null,
            IHttpClientWrapper http = null,
            IJsonSerializer json = null)
        {
            var mockCredsPrv = new Mock<ICredentialsProvider>(MockBehavior.Loose);
            mockCredsPrv.Setup(c => c.GetCredentialsAsync())
                        .Returns(Task.FromResult<ICredentials>(new FakeCredentials()));

            return new Connection(
                productHeader ?? new ProductHeaderValue("Viagogo.Tests", "1.0"),
                credsPrv ?? mockCredsPrv.Object,
                http ?? new Mock<IHttpClientWrapper>(MockBehavior.Loose).Object,
                json ?? new Mock<IJsonSerializer>(MockBehavior.Loose).Object);
        }

        [Test]
        public async void PostAsync_ShouldSendAnHttpRequestMessageWithRequestUriSetToTheGivenUri()
        {
            var expectedUri = new Uri("https://foo.io");
            var mockHttp = new Mock<IHttpClientWrapper>(MockBehavior.Loose);
            mockHttp.Setup(h => h.SendAsync(It.Is<HttpRequestMessage>(r => r.RequestUri == expectedUri),
                                            It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(new HttpResponseMessage()))
                    .Verifiable();
            var conn = CreateConnection(http: mockHttp.Object);

            await conn.PostAsync<string>(expectedUri, null);

            mockHttp.Verify();
        }

        [Test]
        public async void PostAsync_ShouldSendAnHttpRequestMessageWithTheCurrentCredentialsAuthHeader()
        {
            var expectedAuthHeader = "Expected header";
            var mockCredsPrv = new Mock<ICredentialsProvider>(MockBehavior.Loose);
            mockCredsPrv.Setup(c => c.GetCredentialsAsync())
                .Returns(Task.FromResult<ICredentials>(new FakeCredentials(authHeader: expectedAuthHeader)));
            var mockHttp = new Mock<IHttpClientWrapper>(MockBehavior.Loose);
            mockHttp.Setup(h => h.SendAsync(It.Is<HttpRequestMessage>(r => r.Headers.Authorization.ToString() == expectedAuthHeader),
                                            It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(new HttpResponseMessage()))
                    .Verifiable();
            var conn = CreateConnection(http: mockHttp.Object, credsPrv: mockCredsPrv.Object);

            await conn.PostAsync<string>(new Uri("https://api.vgg.io"), null);

            mockHttp.Verify();
        }

        [Test]
        public async void PostAsync_ShouldSendAnHttpRequestMessageWithUserAgentContainingTheGivenProductHeaderValue()
        {
            var expectedUserAgentProduct = ProductHeaderValue.Parse("MyTestApp/0.9.9");
            var mockHttp = new Mock<IHttpClientWrapper>(MockBehavior.Loose);
            mockHttp.Setup(h => h.SendAsync(It.Is<HttpRequestMessage>(r => r.Headers.UserAgent.First().Product.Equals(expectedUserAgentProduct)),
                                            It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(new HttpResponseMessage()))
                    .Verifiable();
            var conn = CreateConnection(http: mockHttp.Object, productHeader: expectedUserAgentProduct);

            await conn.PostAsync<string>(new Uri("https://api.vgg.io"), null);

            mockHttp.Verify();
        }


    }
}
