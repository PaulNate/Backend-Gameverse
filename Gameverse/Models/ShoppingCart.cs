using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public double Price { get; set; }
    [Required]
    public User User { get; set; }
    public int UserId { get; set; }
    public ICollection<Product>? Products { get; set; }
    public Voucher? Voucher { get; set; }

    public bool Done { get; set; } = false;
}