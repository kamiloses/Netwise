namespace Netwise.Services.Impl;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient;

    public HttpClientWrapper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //Added for easier mocking 
    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        
         return await _httpClient.GetAsync(url);
    }
}