using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class Voucher
{
    public int VoucherId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Code { get; set; }
    public int Discount { get; set; }
}