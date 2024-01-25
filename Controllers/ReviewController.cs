using API1.Models;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewsServices _reviewsService;

        public ReviewController(ReviewsServices reviewsServices) =>
            _reviewsService = reviewsServices;

        [HttpGet]
        public async Task<List<Reviews>> Get() =>
            await _reviewsService.GetAsync();

        [HttpGet("{ReviewId}")]
        public async Task<ActionResult<Reviews>> Get(string ReviewId)
        {
            var review = await _reviewsService.GetAsync(ReviewId);

            if (review is null)
            {
                // return NotFound();
            }

            return review;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reviews newReview)
        {
            await _reviewsService.CreateAsync(newReview);

            return CreatedAtAction(nameof(Get), new { ReviewId = newReview.ReviewId }, newReview);
        }


        [HttpPut("{ReviewId}")]
        public async Task<IActionResult> Update(string ReviewId, Reviews updatedReview)
        {
            var reviews = await _reviewsService.GetAsync(ReviewId);

            if (reviews is null)
            {
                return NotFound();
            }

            updatedReview.ReviewId = reviews.ReviewId;

            await _reviewsService.UpdateAsync(ReviewId, updatedReview);

            return NoContent();
        }

        [HttpDelete("{ReviewId}")]
        public async Task<IActionResult> Delete(string ReviewId)
        {
            var review = await _reviewsService.GetAsync(ReviewId);

            if (review is null)
            {
                return NotFound();
            }

            await _reviewsService.RemoveAsync(ReviewId);

            return NoContent();
        }

        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        private IActionResult NoContent()
        {
            throw new NotImplementedException();
        }
    }
}
