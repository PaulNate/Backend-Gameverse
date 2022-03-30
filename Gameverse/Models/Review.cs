using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class Review
{
    public int ReviewId {get; set; }
    [Required]
    public int Grade {get; set;}
    public string ReviewText {get; set;}
    [System.Text.Json.Serialization.JsonIgnore]
    public Product Product {get; set;}
}