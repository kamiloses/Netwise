using Netwise.Dto;

namespace Netwise.Services;

public interface ICatFactService
{
     Task<CatFactResponse> FetchCatFactResponseAsync();
     
     
     
     
}