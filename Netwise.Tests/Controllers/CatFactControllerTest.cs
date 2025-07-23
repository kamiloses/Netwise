

using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Netwise.Controllers;
using Netwise.Dto;
using Xunit;

namespace Netwise.Tests.Controllers;

[TestSubject(typeof(CatFactController))]
public class CatFactControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CatFactControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(); 
    }
    
    
    
    
    
  

    [Fact]
    public async Task GetCatFact_ShouldReturn200AndValidResponse()
    {
        var response = await _client.GetAsync("/api/CatFact");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var catFact = await response.Content.ReadFromJsonAsync<CatFactResponse>();
        catFact.Should().NotBeNull();
        catFact!.Fact.Should().NotBeNullOrWhiteSpace();
    }
    
    
    
    
    
    
}