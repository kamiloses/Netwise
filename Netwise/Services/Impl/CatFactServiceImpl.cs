using Netwise.Dto;
using Netwise.Exceptions;

namespace Netwise.Services.Impl;

public class CatFactServiceImpl
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://catfact.ninja/factxxcz";

    public CatFactServiceImpl(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<CatFactResponse> GetCatFactResponse()
    {

        var response = await _httpClient.GetAsync(Url);

        if (!response.IsSuccessStatusCode)
        {
            throw new ExternalApiException("There was some problem with connecting to the external Api : " + response.ReasonPhrase);
        }


        var data = await response.Content.ReadFromJsonAsync<CatFactResponse>();

       if (data == null || string.IsNullOrWhiteSpace(data.Fact))
       {
           throw new ExternalApiException("There was some problem with fetching data from the external Api.");
       }


       return data;
       
        
        
    }
}