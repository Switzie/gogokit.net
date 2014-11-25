﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Viagogo.Sdk.Authentication;
using Viagogo.Sdk.Clients;
using Viagogo.Sdk.Http;
using Viagogo.Sdk.Models;

namespace Viagogo.Sdk.Tests.Clients
{
    public class OAuth2ClientTests
    {
        private static OAuth2Client CreateClient(
            IConnection conn = null,
            Uri vggUrl = null)
        {
            return new OAuth2Client(
                conn ?? new Mock<IConnection>(MockBehavior.Loose).Object,
                vggUrl ?? new Uri("https://vgg.com/"));
        }

        [Test]
        public async void GetAccessTokenAsync_ShouldPassViagogoUrlWithPathToTokenToTheConnection()
        {
            var expectedUri = new Uri("https://vggBase.io/secure/oauth2/token", UriKind.Absolute);
            var mockConn = new Mock<IConnection>(MockBehavior.Loose);
            mockConn.Setup(c => c.PostAsync<OAuth2Token>(expectedUri, It.IsAny<object>()))
                    .Returns(Task.FromResult<IApiResponse<OAuth2Token>>(new ApiResponse<OAuth2Token>()))
                    .Verifiable();
            var client = CreateClient(conn: mockConn.Object, vggUrl: new Uri("https://vggBase.io/"));

            await client.GetAccessTokenAsync("grantType", null, null);

            mockConn.Verify();
        }

        [Test]
        public async void GetAccessTokenAsync_ShouldPassFormUrlEncodedContentToTheConnection()
        {
            var mockConn = new Mock<IConnection>(MockBehavior.Loose);
            mockConn.Setup(c => c.PostAsync<OAuth2Token>(It.IsAny<Uri>(), It.IsNotNull<FormUrlEncodedContent>()))
                    .Returns(Task.FromResult<IApiResponse<OAuth2Token>>(new ApiResponse<OAuth2Token>()))
                    .Verifiable();
            var client = CreateClient(conn: mockConn.Object);

            await client.GetAccessTokenAsync("grantType", null, null);

            mockConn.Verify();
        }

        [Test]
        public async void GetAccessTokenAsync_ShouldReturnTheBodyOfTheResponseReturnedByTheConnection()
        {
            var expectedToken = new OAuth2Token();
            var mockConn = new Mock<IConnection>(MockBehavior.Loose);
            mockConn.Setup(c => c.PostAsync<OAuth2Token>(It.IsAny<Uri>(), It.IsAny<object>()))
                    .Returns(Task.FromResult<IApiResponse<OAuth2Token>>(new ApiResponse<OAuth2Token> { BodyAsObject = expectedToken }));
            var client = CreateClient(conn: mockConn.Object, vggUrl: new Uri("https://vggBase.io/"));

            var actualToken = await client.GetAccessTokenAsync("grantType", null, null);

            Assert.AreSame(expectedToken, actualToken);
        }
    }
}
