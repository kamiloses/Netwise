using Netwise.Dto;
using Netwise.Exceptions;

namespace Netwise.Services.Impl;

public class CatFactServiceImpl : ICatFactService
{
    private readonly IHttpClientWrapper _httpClient;
    private const string Url = "https://catfact.ninja/fact";
    private const string filePath = "catfacts.txt";

    public CatFactServiceImpl(IHttpClientWrapper httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CatFactResponse> GetCatFactResponseAsync()
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

           CreateLocalFileIfNotExists(data.Fact);
       return data;
       
        
        
    }

    private void CreateLocalFileIfNotExists(string fact)
    {
        string filePath = "catfacts.txt";

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, fact + Environment.NewLine);
        }
        else
        {
            File.AppendAllText(filePath, fact + Environment.NewLine);
        }
    }
}
    