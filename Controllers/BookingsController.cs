using API1.Models;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingsService _bookingsService;

    public BookingsController(BookingsService bookingsService)
    {
        this._bookingsService = bookingsService;
    }

    [HttpGet]
    public async Task<List<Bookings>> Get() =>
        await _bookingsService.GetAsync();



    [HttpGet("{BookingId}")]
    public async Task<ActionResult<Bookings>> Get(string BookingId)
    {
        var bookings = await _bookingsService.GetAsync(BookingId);

        if (bookings is null)
        {
            return NotFound();
        }

        return bookings;
    }
    

    [HttpPost]
    public async Task<IActionResult> Post(Bookings newBookings)
    {
        await _bookingsService.CreateAsync(newBookings);

        return CreatedAtAction(nameof(Get), new { BookingId = newBookings.BookingId }, newBookings);
    }

    [HttpPut("{BookingId}")]
    public async Task<IActionResult> Update(string BookingId, Bookings updatedBookings)
    {
        var bookings = await _bookingsService.GetAsync(BookingId);

        if (bookings is null)
        {
            return NotFound();
        }

        updatedBookings.BookingId = bookings.BookingId;

        await _bookingsService.UpdateAsync(BookingId, updatedBookings);

        return NoContent();
    }

    [HttpDelete("{BookingId}")]
    public async Task<IActionResult> Delete(string BookingId)
    {
        var bookings = await _bookingsService.GetAsync(BookingId);

        if (bookings is null)
        {
            return NotFound();
        }

        await _bookingsService.RemoveAsync(BookingId);

        return NoContent();
    }

    [HttpGet("getbyuser/{username}")]
    public async Task<IActionResult> GetAllBookingsForUsername(string username)
    {
        var bookings = await _bookingsService.GetAllBookingsForUsername(username);
        if (bookings == null)
        {
            return NotFound();
        }
        return Ok(bookings);
    }
}