using Gameverse.Services;
using Gameverse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Gameverse.Controllers;

[ApiController]
[Route("[controller]")]

public class ReviewController : ControllerBase
{
    ReviewService _service;

    public ReviewController(ReviewService service)
    {
        _service = service;
    }

    [HttpGet ("product/{productId}")]
    public IEnumerable<Review> GetByProductId(int productId)
    {
        var Reviews = _service.GetByProductId(productId);
        return Reviews;
    }
}