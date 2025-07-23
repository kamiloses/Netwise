using Microsoft.AspNetCore.Mvc;
using Netwise.Dto;
using Netwise.Services;
using Netwise.Services.Impl;

namespace Netwise.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CatFactController {
    
    private readonly ICatFactService _catFactService; 

    public CatFactController(ICatFactService catFactService)
    {
        _catFactService = catFactService;
    }

    [HttpGet]
    public async Task<CatFactResponse?> GetCatFactResponseAsync()
    {
        return await _catFactService.GetCatFactResponseAsync();


    }
    
    
    
}