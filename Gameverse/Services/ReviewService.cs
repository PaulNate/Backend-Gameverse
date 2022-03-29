using Gameverse.Models;
using Gameverse.Data;
using Microsoft.EntityFrameworkCore;

namespace Gameverse.Services;

public class ReviewService
{
    private readonly GameverseContext _context;

    public ReviewService(GameverseContext context)
    {
        _context = context;
    }

    public IEnumerable<Review> GetByProductId(int ProductId)
    {
        var reviews = _context.Reviews
        .Where(r => r.Product.ProductId == ProductId).ToList();
        return reviews;
    }

    
}