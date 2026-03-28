using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Models;
public class Game
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    [Required]
    [StringLength(50)]
    public required string Genre { get; set; }
    [Range(0.01, 999.99)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}