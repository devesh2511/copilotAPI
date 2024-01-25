using API1.Models;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CleaningServicesController : ControllerBase
{
    private readonly CleaningServicesService _cleaningServicesService;

    public CleaningServicesController(CleaningServicesService cleaningServicesService) =>
        _cleaningServicesService = cleaningServicesService;

    [HttpGet]
    public async Task<List<CleaningServices>> Get() =>
        await _cleaningServicesService.GetAsync();

    [HttpGet("{ServiceId}")]
    public async Task<ActionResult<CleaningServices>> Get(string ServiceId)
    {
        var cleaningServices = await _cleaningServicesService.GetAsync(ServiceId);

        if (cleaningServices is null)
        {
            return NotFound();
        }

        return cleaningServices;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CleaningServices newCleaningServices)
    {
        await _cleaningServicesService.CreateAsync(newCleaningServices);

        return CreatedAtAction(nameof(Get), new { ServiceId = newCleaningServices.ServiceId }, newCleaningServices);
    }

    [HttpPut("{ServiceId}")]
    public async Task<IActionResult> Update(string ServiceId, CleaningServices updatedCleaningServices)
    {
        var cleaningServices = await _cleaningServicesService.GetAsync(ServiceId);

        if (cleaningServices is null)
        {
            return NotFound();
        }

        updatedCleaningServices.ServiceId = cleaningServices.ServiceId;

        await _cleaningServicesService.UpdateAsync(ServiceId, updatedCleaningServices);

        return NoContent();
    }

    [HttpDelete("{ServiceId}")]
    public async Task<IActionResult> Delete(string ServiceId)
    {
        var cleaningServices = await _cleaningServicesService.GetAsync(ServiceId);

        if (cleaningServices is null)
        {
            return NotFound();
        }

        await _cleaningServicesService.RemoveAsync(ServiceId);

        return NoContent();
    }
}