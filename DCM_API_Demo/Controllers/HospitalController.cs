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
    
    [HttpGet("GetAllHospitals")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {

        var claims = User.Claims;
        
        List<Hospital> hospitals = new List<Hospital>();
        
        hospitals.Add(new Hospital{ Id = Guid.NewGuid(), Name = "Hos 1", State = "NSW", HeadDoctor = "Doc 1"});
        hospitals.Add(new Hospital{ Id = Guid.NewGuid(), Name = "Hos 2", State = "VIC", HeadDoctor = "Doc 2"});

        return Ok(hospitals);

    }
    
    [HttpGet("GetAllDoctors")]
    [Authorize(Policy = "IsVicDoctor")]
    public async Task<IActionResult> GetDoctors()
    {

        var claims = User.Claims;
        
        List<Doctor> docs = new List<Doctor>();
        
        docs.Add(new Doctor{ Id = Guid.NewGuid(), Name = "Doc 1", Speciality = "Cardiology"});
        docs.Add(new Doctor{ Id = Guid.NewGuid(), Name = "Doc 2", Speciality = "Dentist"});

        return Ok(docs);

    }
}