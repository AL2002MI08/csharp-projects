using GameCatalogApi.Data;
using GameCatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCatalogApi.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            SeedDataIfEmpty();
        }

        private void SeedDataIfEmpty()
        {
            if (_context.Games.Any())
                return;

            var games = new List<Game>
            {
                new Game { Title = "Realm of Shadows", Developer = "Mystic Studios", Genre = "RPG", Price = 39.99M, ReleaseYear = 2018, IsMultiplayer = false },
                new Game { Title = "Victory League", Developer = "SportSimulation Inc", Genre = "Sports", Price = 59.99M, ReleaseYear = 2022, IsMultiplayer = true },
                new Game { Title = "Block Builders", Developer = "Cube Games", Genre = "Sandbox", Price = 29.99M, ReleaseYear = 2017, IsMultiplayer = true },
                new Game { Title = "Neo City 2050", Developer = "Future Works", Genre = "RPG", Price = 49.99M, ReleaseYear = 2021, IsMultiplayer = false },
                new Game { Title = "The Blood of Dawnwalker ", Developer = "Rebel Wolves Studio", Genre = "Dark Fantasy", Price = 70.00M, ReleaseYear = 2026, IsMultiplayer = true }
            };

            _context.Games.AddRange(games);
            _context.SaveChanges();

            var reviews = new List<Review>
            {
                new Review { GameId = games[0].Id, ReviewerName = "GamerX42", Comment = "Incredible story and atmospheric world design. The magic system is innovative!", Rating = 9, ReviewDate = DateTime.Parse("2019-05-20") },
                new Review { GameId = games[1].Id, ReviewerName = "SportsGamer99", Comment = "Best sports simulation I've played this year. Physics engine is spot on!", Rating = 8, ReviewDate = DateTime.Parse("2022-10-15") },
                new Review { GameId = games[2].Id, ReviewerName = "BuilderPro", Comment = "Endless creativity in this game. The building mechanics are so intuitive.", Rating = 10, ReviewDate = DateTime.Parse("2020-01-10") },
                new Review { GameId = games[3].Id, ReviewerName = "CyberPlayer", Comment = "The futuristic setting is breathtaking but combat needs some work.", Rating = 7, ReviewDate = DateTime.Parse("2021-12-05") },
                new Review { GameId = games[4].Id, ReviewerName = "ActionSeeker", Comment = "Massive open world with tons of activities. Never gets boring!", Rating = 9, ReviewDate = DateTime.Parse("2026-09-05") }
            };

            _context.Reviews.AddRange(reviews);
            _context.SaveChanges();
        }

        public List<Game> GetAllGames() => _context.Games.ToList();

        public Game? GetGame(int id) => _context.Games.FirstOrDefault(g => g.Id == id);

        public List<Game> SearchGames(string? title, string? genre, int? releaseYear)
        {
            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(g => g.Title.ToLower().Contains(title.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(g => g.Genre.ToLower() == genre.ToLower());
            }

            if (releaseYear.HasValue)
            {
                query = query.Where(g => g.ReleaseYear == releaseYear.Value);
            }

            return query.ToList();
        }

        public Game CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }

        public bool UpdateGame(int id, Game game)
        {
            var existingGame = _context.Games.FirstOrDefault(g => g.Id == id);
            if (existingGame == null)
                return false;

            existingGame.Title = game.Title;
            existingGame.Developer = game.Developer;
            existingGame.Genre = game.Genre;
            existingGame.Price = game.Price;
            existingGame.ReleaseYear = game.ReleaseYear;
            existingGame.IsMultiplayer = game.IsMultiplayer;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteGame(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
                return false;

            var reviews = _context.Reviews.Where(r => r.GameId == id).ToList();
            if (reviews.Any())
            {
                _context.Reviews.RemoveRange(reviews);
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
            return true;
        }

        public List<Review> GetReviewsByGameId(int gameId) => 
            _context.Reviews.Where(r => r.GameId == gameId).ToList();

        public Review? GetReview(int id) => 
            _context.Reviews.FirstOrDefault(r => r.Id == id);

        public Review CreateReview(Review review)
        {
            review.ReviewDate = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public bool DeleteReview(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return true;
        }
    }
}