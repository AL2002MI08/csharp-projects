using GameCatalogApi.Models;

namespace GameCatalogApi.Services
{
    public class GameService
    {
        private static List<Game> _games = new List<Game>
        {
            new Game { 
                Id = 1, 
                Title = "Realm of Shadows", 
                Developer = "Mystic Studios", 
                Genre = "RPG", 
                Price = 39.99M, 
                ReleaseYear = 2018, 
                IsMultiplayer = false 
            },
            new Game { 
                Id = 2, 
                Title = "Victory League", 
                Developer = "SportSimulation Inc", 
                Genre = "Sports", 
                Price = 59.99M, 
                ReleaseYear = 2022, 
                IsMultiplayer = true 
            },
            new Game { 
                Id = 3, 
                Title = "Block Builders", 
                Developer = "Cube Games", 
                Genre = "Sandbox", 
                Price = 29.99M, 
                ReleaseYear = 2017, 
                IsMultiplayer = true 
            },
            new Game { 
                Id = 4, 
                Title = "Neo City 2050", 
                Developer = "Future Works", 
                Genre = "RPG", 
                Price = 49.99M, 
                ReleaseYear = 2021, 
                IsMultiplayer = false 
            },
            new Game { 
                Id = 5, 
                Title = "Urban Chaos", 
                Developer = "Open World Studios", 
                Genre = "Action", 
                Price = 29.99M, 
                ReleaseYear = 2019, 
                IsMultiplayer = true 
            }
        };

        private static List<Review> _reviews = new List<Review>
        {
            new Review { 
                Id = 1, 
                GameId = 1, 
                ReviewerName = "GamerX42", 
                Comment = "Incredible story and atmospheric world design. The magic system is innovative!", 
                Rating = 9,
                ReviewDate = DateTime.Parse("2019-05-20") 
            },
            new Review { 
                Id = 2, 
                GameId = 2, 
                ReviewerName = "SportsGamer99", 
                Comment = "Best sports simulation I've played this year. Physics engine is spot on!", 
                Rating = 8,
                ReviewDate = DateTime.Parse("2022-10-15") 
            },
            new Review { 
                Id = 3, 
                GameId = 3, 
                ReviewerName = "BuilderPro", 
                Comment = "Endless creativity in this game. The building mechanics are so intuitive.", 
                Rating = 10,
                ReviewDate = DateTime.Parse("2020-01-10") 
            },
            new Review { 
                Id = 4, 
                GameId = 4, 
                ReviewerName = "CyberPlayer", 
                Comment = "The futuristic setting is breathtaking but combat needs some work.", 
                Rating = 7,
                ReviewDate = DateTime.Parse("2021-12-05") 
            },
            new Review { 
                Id = 5, 
                GameId = 5, 
                ReviewerName = "ActionSeeker", 
                Comment = "Massive open world with tons of activities. Never gets boring!", 
                Rating = 9,
                ReviewDate = DateTime.Parse("2020-03-18") 
            }
        };

        private static int _nextGameId = 6;
        private static int _nextReviewId = 6;

        // Game methods
        public List<Game> GetAllGames() => _games;

        public Game? GetGame(int id) => _games.FirstOrDefault(g => g.Id == id);

        public List<Game> SearchGames(string? title, string? genre, int? releaseYear)
        {
            var result = _games.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(g => g.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                result = result.Where(g => g.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (releaseYear.HasValue)
            {
                result = result.Where(g => g.ReleaseYear == releaseYear.Value);
            }

            return result.ToList();
        }

        public Game CreateGame(Game game)
        {
            game.Id = _nextGameId++;
            _games.Add(game);
            return game;
        }

        public bool UpdateGame(int id, Game game)
        {
            var existingGame = GetGame(id);
            if (existingGame == null)
                return false;

            existingGame.Title = game.Title;
            existingGame.Developer = game.Developer;
            existingGame.Genre = game.Genre;
            existingGame.Price = game.Price;
            existingGame.ReleaseYear = game.ReleaseYear;
            existingGame.IsMultiplayer = game.IsMultiplayer;

            return true;
        }

        public bool DeleteGame(int id)
        {
            var game = GetGame(id);
            if (game == null)
                return false;

            _games.Remove(game);

            // Also remove all reviews for this game
            _reviews.RemoveAll(r => r.GameId == id);

            return true;
        }

        // Review methods
        public List<Review> GetReviewsByGameId(int gameId) => 
            _reviews.Where(r => r.GameId == gameId).ToList();

        public Review? GetReview(int id) => 
            _reviews.FirstOrDefault(r => r.Id == id);

        public Review CreateReview(Review review)
        {
            review.Id = _nextReviewId++;
            review.ReviewDate = DateTime.Now;
            _reviews.Add(review);
            return review;
        }

        public bool DeleteReview(int id)
        {
            var review = GetReview(id);
            if (review == null)
                return false;

            _reviews.Remove(review);
            return true;
        }
    }
}