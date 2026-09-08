using Microsoft.AspNetCore.Mvc;
using GameCatalogApi.Models;
using GameCatalogApi.Services;
using System.Collections.Generic;

namespace GameCatalogApi.Controllers
{
    [ApiController]
    [Route("api/Games")]
    public class GamesController : ControllerBase
    {
        private readonly GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<List<Game>> GetAll(){
          return _gameService.GetAllGames();
        }

        [HttpGet("{id}")]
        public ActionResult<Game> GetById(int id)
        {
            var game = _gameService.GetGame(id);
            
            if (game == null)
                return NotFound($"Game with ID {id} not found");
                
            return game;
        }

        [HttpGet("search")]
        public ActionResult<List<Game>> Search(
            [FromQuery] string? title, 
            [FromQuery] string? genre, 
            [FromQuery] int? releaseYear
            )
        {
            var games = _gameService.SearchGames(title, genre, releaseYear);
            
            if (games.Count == 0)
                return new List<Game>();
                
            return games;
        }

        [HttpPost]
        public ActionResult<Game> Create(Game game)
        {
            var newGame = _gameService.CreateGame(game);
            
            return CreatedAtAction(nameof(GetById), new { id = newGame.Id }, newGame);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Game game)
        {
            if (id != game.Id)
                return BadRequest("ID mismatch between URL and game object");
                
            var updated = _gameService.UpdateGame(id, game);
            if(updated == false){
              return NotFound($"Game with ID {id} not found");
            }
                
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _gameService.DeleteGame(id);
            
            if (!deleted)
                return NotFound($"Game with ID {id} not found");
                
            return NoContent();
        }

    }
}