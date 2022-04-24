using Gameverse.Models;
using Gameverse.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gameverse.Services;

public class ShoppingCartsService
{
    private readonly GameverseContext _context;

    public ShoppingCartsService(GameverseContext context)
    {
        _context = context;
    }

    public ShoppingCart? GetById(int id)
    {
        var shoppingCart = _context.ShoppingCarts
            .Include(x => x.User)
            .ThenInclude(x => x.Role)
            .Include(x => x.Products)
            .AsNoTracking()
            .SingleOrDefault(p => p.ShoppingCartId == id);
        return shoppingCart;
    }
    public ShoppingCart Create(ShoppingCartDto shoppingCart)
    {
        var newShoppingCart = new ShoppingCart()
        {
            Price = 0,
            User = _context.Users.Find(shoppingCart.UserId),
            Products = new List<Product>()
        };
        _context.ShoppingCarts.Add(newShoppingCart);
        _context.SaveChanges();

        return newShoppingCart;
    }

    public ShoppingCart AddProduct(int shoppingCartId, int productId, int Quantity)
    {
        var shoppingCart = _context.ShoppingCarts
            .Include(p => p.Products)
            .Include(p => p.User)
            .ThenInclude(u => u.Role)
            .SingleOrDefault(p => p.ShoppingCartId == shoppingCartId);

        if (shoppingCart != null)
        {
            if (shoppingCart.Products != null)
            {
                var product = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Reviews)
                    .SingleOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    if (shoppingCart.Products.Contains(product))
                    {
                        product.Quantity += Quantity;
                        shoppingCart.Products.Add(product);
                        shoppingCart.Price += (double)product.Quantity * product.Price;
                    }
                    else
                    {
                        product.Quantity = Quantity;
                        shoppingCart.Products.Add(product);
                        shoppingCart.Price += (double)product.Quantity * product.Price;
                    }
                }
                else
                {
                    throw new NullReferenceException("Product doesn't exist!");
                }
            }
            else
            {
                throw new NullReferenceException("List doesn't exist!");
            }
        }
        else
        {
            throw new NullReferenceException("Shopping Cart doesn't exist");
        }
        _context.SaveChanges();
        return shoppingCart;
    }

    public ShoppingCart ClearShoppingCart(int shoppingCartId)
    {
        var shoppingCart = _context.ShoppingCarts
           .Include(p => p.Products)
           .Include(p => p.User)
           .ThenInclude(u => u.Role)
           .SingleOrDefault(p => p.ShoppingCartId == shoppingCartId);

        if (shoppingCart != null)
        {
            if (shoppingCart.Products != null)
            {
                shoppingCart.Price = 0;
                shoppingCart.Products.Clear();
            }
        }
        else
        {
            throw new NullReferenceException("Shopping cart doesn't exist");
        }
        _context.SaveChanges();
        return shoppingCart;
    }

    public ShoppingCart DeleteProduct(int shoppingCartId, int productId)
    {
        var shoppingCart = _context.ShoppingCarts
            .Include(p => p.Products)
            .Include(p => p.User)
            .ThenInclude(u => u.Role)
            .SingleOrDefault(p => p.ShoppingCartId == shoppingCartId);
        if (shoppingCart != null)
        {
            if (shoppingCart.Products != null)
            {
                var product = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Reviews)
                    .SingleOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    if (shoppingCart.Products.Contains(product))
                    {
                        shoppingCart.Products.Remove(product);
                        shoppingCart.Price -= (double)product.Quantity * product.Price;
                    }
                }
            }
        }
        _context.SaveChanges();
        return shoppingCart;
    }

    public ShoppingCart UpdateQuantity(int shoppingCartId, int productId, int newQuantity)
    {
        var shoppingCart = _context.ShoppingCarts
           .Include(p => p.Products)
           .Include(p => p.User)
           .ThenInclude(u => u.Role)
           .SingleOrDefault(p => p.ShoppingCartId == shoppingCartId);
        var productToUpdate = _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.Category)
            .SingleOrDefault(p => p.ProductId == productId);
        if (shoppingCart != null)
        {
            if (shoppingCart.Products != null)
            {
                if (productToUpdate != null)
                {
                    var productsList = shoppingCart.Products.ToList();
                    if (productsList.Contains(productToUpdate))
                    {
                        var index = productsList.IndexOf(productToUpdate);
                        var oldQuantity = productsList[index].Quantity;
                        var difQuantity = newQuantity - oldQuantity;
                        shoppingCart.Price += difQuantity * productToUpdate.Price;
                        productsList[index].Quantity = newQuantity;
                        shoppingCart.Products = productsList;
                    }
                }
            }
        }
        _context.SaveChanges();
        return shoppingCart;
    }
}