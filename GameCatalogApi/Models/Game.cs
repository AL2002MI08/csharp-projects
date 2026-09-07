using System.ComponentModel.DataAnnotations;

namespace GameCatalogApi.Models
{
    public class Game
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength=3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Developer { get; set; } = string.Empty;
        
        [Required]
        public string Genre { get; set; } = string.Empty;
        
        [Range(0, 100)]
        public decimal Price { get; set; }
        
        [Range(1980, 2030)]
        public int ReleaseYear { get; set; }
        
        public bool IsMultiplayer { get; set; }
    }
}