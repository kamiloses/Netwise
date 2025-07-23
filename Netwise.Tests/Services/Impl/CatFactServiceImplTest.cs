using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Moq;
using Netwise.Dto;
using Netwise.Exceptions;
using Netwise.Services;
using Netwise.Services.Impl;
using Xunit;

namespace Netwise.Tests.Services.Impl;

[TestSubject(typeof(CatFactServiceImpl))]
public class CatFactServiceImplTest {
    
 [Fact]
public async Task GetCatFactResponseAsync_ThrowsExternalApiException_WhenFactIsNull()
{
    var mockHttpClient = new Mock<IHttpClientWrapper>();
    var badResponse = new CatFactResponse { Fact = null };

    var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = JsonContent.Create(badResponse)
    };

    mockHttpClient.Setup(c => c.GetAsync(It.IsAny<string>()))
        .ReturnsAsync(httpResponse);

    var service = new CatFactServiceImpl(mockHttpClient.Object);

    var ex = await Assert.ThrowsAsync<ExternalApiException>(() => service.GetCatFactResponseAsync());
    Assert.Contains("There was some problem with fetching data from the external Api", ex.Message);
}

[Fact]
public async Task GetCatFactResponseAsync_ThrowsExternalApiException_WhenFactIsEmpty()
{
    var mockHttpClient = new Mock<IHttpClientWrapper>();
    var badResponse = new CatFactResponse { Fact = "" };

    var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = JsonContent.Create(badResponse)
    };

    mockHttpClient.Setup(c => c.GetAsync(It.IsAny<string>()))
        .ReturnsAsync(httpResponse);

    var service = new CatFactServiceImpl(mockHttpClient.Object);

    var ex = await Assert.ThrowsAsync<ExternalApiException>(() => service.GetCatFactResponseAsync());
    Assert.Contains("There was some problem with fetching data from the external Api", ex.Message);
}

[Fact]
public async Task GetCatFactResponseAsync_ThrowsExternalApiException_WhenFactIsWhitespace()
{
    var mockHttpClient = new Mock<IHttpClientWrapper>();
    var badResponse = new CatFactResponse { Fact = "   " };

    var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = JsonContent.Create(badResponse)
    };

    mockHttpClient.Setup(c => c.GetAsync(It.IsAny<string>()))
        .ReturnsAsync(httpResponse);

    var service = new CatFactServiceImpl(mockHttpClient.Object);

    var ex = await Assert.ThrowsAsync<ExternalApiException>(() => service.GetCatFactResponseAsync());
    Assert.Contains("There was some problem with fetching data from the external Api", ex.Message);
}
}
