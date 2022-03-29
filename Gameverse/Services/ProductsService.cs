using Gameverse.Models;
using Gameverse.Data;
using Microsoft.EntityFrameworkCore;

namespace Gameverse.Services;

public class ProductsService
{
    private readonly GameverseContext _context;

    public ProductsService(GameverseContext context)
    {
        _context = context;
    }

    public Product? GetById(int id)
    {
        var product = _context.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .SingleOrDefault(p => p.ProductId == id);
        return product;
    }
    public Product Create(ProductDto newProduct)
    {
        var product = new Product();
        product.Name = newProduct.Name;
        product.Price = newProduct.Price;
        product.Description = newProduct.Description;
        product.Category = _context.Categories.Find(newProduct.CategoryId);
        _context.Products.Add(product);
        _context.SaveChanges();

        return product;
    }
    public IEnumerable<Product> GetProducts()
    {
        var products = _context.Products.ToList();
        return products;
    }
    public IEnumerable<Product> GetByPriceMax(double price)
    {
        var products = _context.Products
        .Where(x => x.Price <= price)
        .ToList();
        return products;
    }
    public IEnumerable<Product> GetByCategory(int categoryId)
    {
        var products = _context.Products
        .Where(c => c.Category.CategoryId == categoryId).ToList();
        return products;
    }
    public IEnumerable<Product> GetByShoppingCart(int ShoppingCartId)
    {
        var products = _context.Products.
        Include(x => x.ProductShoppingCarts)
        .ThenInclude(x => x.ShoppingCartId);
        return products;
    }
    public void AddToShoppingCart(int ProductId, int ShoppingCartId)
    {
        var productToUpdate = _context.Products.Find(ProductId);
        var destinationShoppingCart = _context.ShoppingCarts.Find(ShoppingCartId);

        if (productToUpdate is null || destinationShoppingCart is null)
        {
            throw new NullReferenceException("Product or ShoppingCart does not exist");
        }

        productToUpdate.ProductShoppingCarts.Add(new ProductShoppingCart{
            Product = productToUpdate,
            ShoppingCart = destinationShoppingCart,
        });

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var productToDelete = _context.Products.Find(id);
        if (productToDelete is not null)
        {
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }        
    }

    public Product UpdateProduct(int id, ProductDto newProduct)
    {
        var productToUpdate = _context.Products.Find(id);

        if(newProduct.Name != null)
        {
            productToUpdate.Name = newProduct.Name;
        }
        if(newProduct.Description != null)
        {
            productToUpdate.Description = newProduct.Description;
        }
        if(newProduct.Price != null)
        {
            productToUpdate.Price = newProduct.Price;
        }
        if(newProduct.Quantity != null)
        {
            productToUpdate.Quantity = newProduct.Quantity;
        }

        _context.Products.Update(productToUpdate);
        _context.SaveChanges();

        return productToUpdate;
    }

    public double GetAverageReview(int productId)
    {   
        var productToCompute = _context.Products.Include(p => p.Reviews).FirstOrDefault(p => p.ProductId == productId);
    
        if(productToCompute != null && productToCompute.Reviews != null)
        {
            return productToCompute.Reviews.Average(a => a.Grade);
        }
        throw new NullReferenceException("Error");
    }

    public IEnumerable<Product> GetProductsByAverageReview()
    {
        var products = _context.Products
            .Include(p => p.Reviews)
            .OrderByDescending(p => p.Reviews
            .Average(p => p.Grade));
        return products;
    }
}