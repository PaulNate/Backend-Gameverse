using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class ProductDto
{

    [MaxLength(100)]
    public string? Name { get; set; }

    public int Quantity { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
}