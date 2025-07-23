using Netwise.Dto;
using Netwise.Exceptions;

namespace Netwise.Services.Impl;

public class CatFactServiceImpl : ICatFactService
{
    private readonly IHttpClientWrapper _httpClient;
    private readonly ILogger<CatFactServiceImpl> _logger;
    private const string Url = "https://catfact.ninja/fact";

    public CatFactServiceImpl(IHttpClientWrapper httpClient, ILogger<CatFactServiceImpl> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CatFactResponse> FetchCatFactResponseAsync()
    {

        var response = await _httpClient.GetAsync(Url);
        
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Error with connecting to external Api {Url}");
            throw new ExternalApiException($"There was a problem connecting to the external API. Reason: {response.ReasonPhrase}");
        }


        var data = await response.Content.ReadFromJsonAsync<CatFactResponse>();

        
       if (data == null || string.IsNullOrWhiteSpace(data.Fact))
       {
           _logger.LogError($"Error while fetching data from {Url}");
           throw new ExternalApiException("There was some problem with fetching data from the external Api.");
       }
       
       _logger.LogInformation($"Saved {data.Fact} to file");
       
          return data;
    }
    
}
    