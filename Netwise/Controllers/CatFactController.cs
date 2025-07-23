using Microsoft.AspNetCore.Mvc;
using Netwise.Dto;
using Netwise.Services;
using Netwise.Services.Impl;

namespace Netwise.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CatFactController {
    
    private readonly CatFactServiceImpl _catFactService; //todo zamienic na interfejs a nie impl

    public CatFactController(CatFactServiceImpl catFactService)
    {
        _catFactService = catFactService;
    }


    [HttpGet]
    public async Task<CatFactResponse?> GetCatFactResponse()
    {
        return await _catFactService.GetCatFactResponse();


    }
    
    
    
}