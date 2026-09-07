using System.ComponentModel.DataAnnotations;

namespace GameCatalogApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        public int GameId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ReviewerName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = string.Empty;
        
        [Range(1, 10)]
        public int Rating { get; set; }
        
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}