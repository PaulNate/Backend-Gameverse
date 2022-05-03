using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class ShoppingCartDto
{
    public int ShoppingCartId { get; set; }

    public int? Price { get; set; }
    [Required]
    public int UserId { get; set; }
    public List<ProductShoppingCart>? ProductShoppingCarts { get; set; }
    public bool Done { get; set; } = false;
}