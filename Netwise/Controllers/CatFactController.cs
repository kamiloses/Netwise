using Microsoft.AspNetCore.Mvc;
using Netwise.Dto;
using Netwise.Services;

namespace Netwise.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatFactController
{
    private readonly ICatFactService _catFactService;
    private readonly IFileStorageService _fileStorageService;

    public CatFactController(ICatFactService catFactService, IFileStorageService fileStorageService)
    {
        _catFactService = catFactService;
        _fileStorageService = fileStorageService;
    }

    [HttpGet]
    public async Task<CatFactResponse> GetCatFactResponseAsync()
    {
        CatFactResponse data = await _catFactService.FetchCatFactResponseAsync();

      await  _fileStorageService.SaveToFile(data.Fact);

        return data;
    }
}