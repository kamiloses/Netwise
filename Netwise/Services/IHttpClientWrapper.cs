namespace Netwise.Services;

public interface IHttpClientWrapper
{
     Task<HttpResponseMessage> GetAsync(string url);
}