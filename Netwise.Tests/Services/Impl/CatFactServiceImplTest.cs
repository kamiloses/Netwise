using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;
using Netwise.Dto;
using Netwise.Exceptions;
using Netwise.Services;
using Netwise.Services.Impl;
using Xunit;

namespace Netwise.Tests.Services.Impl;

[TestSubject(typeof(CatFactServiceImpl))]
public class CatFactServiceImplTest
{
    private static CatFactServiceImpl CreateServiceWithResponse(HttpResponseMessage response)
    {
        var mockHttpClient = new Mock<IHttpClientWrapper>();
        mockHttpClient.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

        var mockLogger = new Mock<ILogger<CatFactServiceImpl>>();

        return new CatFactServiceImpl(mockHttpClient.Object, mockLogger.Object);
    }

    [Fact]
    public async Task FetchCatFactResponseAsync_ThrowsExternalApiException_WhenFactIsNull()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new CatFactResponse { Fact = null })
        };

        var service = CreateServiceWithResponse(response);

        var ex = await Assert.ThrowsAsync<ExternalApiException>(() => service.FetchCatFactResponseAsync());
        Assert.Contains("There was some problem with fetching data from the external Api", ex.Message);
    }

    [Fact]
    public async Task FetchCatFactResponseAsync_ThrowsExternalApiException_WhenFactIsEmpty()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new CatFactResponse { Fact = "" })
        };

        var service = CreateServiceWithResponse(response);

        var ex = await Assert.ThrowsAsync<ExternalApiException>(() => service.FetchCatFactResponseAsync());
        Assert.Contains("There was some problem with fetching data from the external Api", ex.Message);
    }

    [Fact]
    public async Task FetchCatFactResponseAsync_ThrowsExternalApiException_WhenFactIsWhitespace()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new CatFactResponse { Fact = "   " })
        };

        var service = CreateServiceWithResponse(response);

        var ex = await Assert.ThrowsAsync<ExternalApiException>(() => service.FetchCatFactResponseAsync());
        Assert.Contains("There was some problem with fetching data from the external Api", ex.Message);
    }

    [Fact]
    public async Task FetchCatFactResponseAsync_ShouldReturnValidResponse()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new CatFactResponse
            {
                Fact = "Cats sleep 70% of their lives",
                Length = 30
            })
        };

        var service = CreateServiceWithResponse(response);

        var result = await service.FetchCatFactResponseAsync();

        Assert.NotNull(result);
        Assert.Equal("Cats sleep 70% of their lives", result.Fact);
        Assert.Equal(30, result.Length);
    }
}
