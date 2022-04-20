using Gameverse.Services;
using Gameverse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Gameverse.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    ShoppingCartsService _service;

    public ShoppingCartController(ShoppingCartsService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public ActionResult<ShoppingCart> GetById(int id)
    {
        var shoppingCart = _service.GetById(id);

        if (shoppingCart is not null)
        {
            return shoppingCart;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] ShoppingCartDto newShoppingCart)
    {
        var shoppingCart = _service.Create(newShoppingCart);
        return CreatedAtAction(nameof(GetById), new { id = shoppingCart!.ShoppingCartId }, shoppingCart);
    }

    [HttpPut("addProduct")]
    public ShoppingCart AddProduct(int productId, int shoppingCartId, int Quantity)
    {
        var shoppingCart = _service.AddProduct(shoppingCartId, productId, Quantity);
        return shoppingCart;
    }

    [HttpDelete("clearShoppingCart")]
    public ShoppingCart ClearShoppingCart(int shoppingCartId)
    {
        var shoppingCart = _service.ClearShoppingCart(shoppingCartId);
        return shoppingCart;
    }

    [HttpDelete("deleteProduct")]
    public ShoppingCart DeleteProduct(int shoppingCartId, int productId)
    {
        var shoppingCart = _service.DeleteProduct(shoppingCartId, productId);
        return shoppingCart;
    }

    [HttpPut("updateQuantity")]
    public ShoppingCart UpdateQuantity(int shoppingCartId, int productId, int newQuantity)
    {
        var shoppingCart = _service.UpdateQuantity(shoppingCartId, productId, newQuantity);
        return shoppingCart;
    }
}
