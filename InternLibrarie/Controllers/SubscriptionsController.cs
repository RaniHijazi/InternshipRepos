using InternshipLibrarie.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternshipLibrarie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController:Controller
    {
        private readonly ISubscriptionsRepository _subscriptionRepository;

        public SubscriptionsController(ISubscriptionsRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        [HttpPost("renew/{userId}")]
        public async Task<IActionResult> RenewSubscription(int userId)
        {
            try
            {
                await _subscriptionRepository.RenewSubscriptionAsync(userId);
                return Ok("Subscription renewed successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


    }
}
