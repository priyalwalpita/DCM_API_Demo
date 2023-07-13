using DCM_API_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCM_API_Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class HospitalController : ControllerBase
{
    private readonly ILogger<HospitalController> _logger;

    public HospitalController(ILogger<HospitalController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetAllHospitals")]
    [Authorize]
    public async Task<IActionResult> Get()
    {

        var claims = User.Claims;
        
        List<Hospital> hospitals = new List<Hospital>();
        
        hospitals.Add(new Hospital{ Id = Guid.NewGuid(), Name = "Hos 1", State = "NSW", HeadDoctor = "Doc 1"});
        hospitals.Add(new Hospital{ Id = Guid.NewGuid(), Name = "Hos 2", State = "VIC", HeadDoctor = "Doc 2"});

        return Ok(hospitals);

    }
}