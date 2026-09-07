using System.ComponentModel.DataAnnotations;

namespace GameCatalogApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
        public string Developer { get; set; } = string.Empty;
        
        public string Genre { get; set; } = string.Empty;
        
        [Range(0, 100)]
        public decimal Price { get; set; }
        
        [Range(1980, 2030)]
        public int ReleaseYear { get; set; }
        
        public bool IsMultiplayer { get; set; }
    }
}