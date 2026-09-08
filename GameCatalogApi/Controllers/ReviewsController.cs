using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameCatalogApi.Models;
using GameCatalogApi.Services;
using System.Collections.Generic;

namespace GameCatalogApi.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly GameService _gameService;

        public ReviewsController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("game/{gameId}")]
        [Authorize(Roles = "Gamer,Developer,Admin")]
        public ActionResult<List<Review>> GetByGameId(int gameId)
        {
            var game = _gameService.GetGame(gameId);
            if (game == null)
                return NotFound($"Game with ID {gameId} not found");

            return _gameService.GetReviewsByGameId(gameId);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Gamer,Developer,Admin")]
        public ActionResult<Review> GetById(int id)
        {
            var review = _gameService.GetReview(id);
            if (review == null)
                return NotFound($"Review with ID {id} not found");

            return review;
        }

        [HttpPost]
        [Authorize(Roles = "Gamer,Developer,Admin")]
        public ActionResult<Review> Create([FromBody] Review review)
        {
            var game = _gameService.GetGame(review.GameId);
            if (game == null)
                return BadRequest($"Cannot create review. Game with ID {review.GameId} does not exist.");

            var createdReview = _gameService.CreateReview(review);
            return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Gamer,Developer,Admin")]
        public IActionResult Delete(int id)
        {
            var deleted = _gameService.DeleteReview(id);
            if (!deleted)
                return NotFound($"Review with ID {id} not found");

            return NoContent();
        }
    }
}
