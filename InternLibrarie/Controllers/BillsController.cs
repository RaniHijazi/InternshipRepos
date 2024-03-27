using InternshipLibrarie.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BillsController : ControllerBase
{
    private readonly IBillsRepository _billsRepository;
    public readonly DataContext context;

    public BillsController(IBillsRepository billsRepository)
    {
        _billsRepository = billsRepository;
        this.context = context;
    }

    [HttpPost("create/{borrowId}")]
    public async Task<IActionResult> CreateBill(int borrowId)
    {
        try
        {
            var billDto = await _billsRepository.CreateBillAsync(borrowId);
            return Ok(billDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpGet("bill/{userId}")]
    public async Task<IActionResult> GetBillsForUserAsync(int userId)
    {
        try
        {
            var bills = await _billsRepository.GetBillsForUserAsync(userId);
            if (bills == null || bills.Count == 0)
            {
                return NotFound("No bills found for the specified user.");
            }
            return Ok(bills);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred: " + ex.Message);
        }
    }
}
