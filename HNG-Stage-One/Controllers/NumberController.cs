using System.Runtime.InteropServices.JavaScript;
using HNG_Stage_One.DTO;
using HNG_Stage_One.Services;
using Microsoft.AspNetCore.Mvc;

namespace HNG_Stage_One.Controllers;

[Route("api/classify-number")]
[ApiController]
public class NumberController : ControllerBase
{
    private readonly NumberService _numberService;

    public NumberController(NumberService numberService)
    {
        _numberService = numberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNumberClassification([FromQuery] string number)
    {
        var response = await _numberService.GetNumberClassificationAsync(number);

        // Check if response is an ErrorResponse
        if (response is ErrorResponse errorResponse)
            return BadRequest(errorResponse);

        return Ok(response);
    }

        
    
}