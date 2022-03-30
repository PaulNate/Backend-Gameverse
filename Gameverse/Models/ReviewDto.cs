using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class ReviewDto
{
    public int ReviewId {get; set; }
    [Required]
    public int Grade {get; set;}
    public string ReviewText {get; set;}
    public int ProductId {get; set;}
}