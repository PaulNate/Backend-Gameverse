using Gameverse.Models;
using Gameverse.Data;
using Microsoft.EntityFrameworkCore;

namespace Gameverse.Services;

public class UserService
{
    private readonly GameverseContext _context;

    public UserService(GameverseContext context)
    {
        _context = context;
    }

    public User? GetById(int id)
    {
        var user = _context.Users
            .Include(p => p.Role)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
        return user;
    }

    public User? GetUserExists(string email, string password)
    {
        var user = _context.Users
        .Include(p => p.Role)
        .AsNoTracking()
        .SingleOrDefault(p => p.Email == email && p.Password == password);

        return user;
    }
    public User Create(UserDto newUser)
    {
        var user = new User();
        user.Name = newUser.Name;
        user.Email = newUser.Email;
        user.Address = newUser.Address;
        user.Phone = newUser.Phone;
        user.Password = newUser.Password;
        user.Role = _context.Roles.Find(newUser.roleId);
        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public void SetRole(int UserId, int RoleId)
    {
        var userToUpdate = _context.Users.Find(UserId);
        var roleToAdd = _context.Roles.Find(RoleId);

        if (userToUpdate is null || roleToAdd is null)
        {
            throw new NullReferenceException("User or role does not exist");
        }

        if (userToUpdate.Role is null)
        {
            userToUpdate.Role = new Role();
        }

        userToUpdate.Role = roleToAdd;

        _context.Users.Update(userToUpdate);
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var userToDelete = _context.Users.Find(id);
        if (userToDelete is not null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Role> GetRoles()
    {
        var roles = _context.Roles.ToList();
        if (roles is null)
        {
            throw new NullReferenceException("No roles");
        }

        return roles;
    }

    public IEnumerable<ShoppingCart> GetPurchaseHistory(int userId)
    {
        var shoppingCarts = _context.ShoppingCarts
            .Include(p => p.Products)
            .Include(p => p.User)
            .ThenInclude(u => u.Role)
            .Where(sc => sc.UserId == userId)
            .Where(sc => sc.Price != 0)
            .ToList();
        if (shoppingCarts != null)
        {
            return shoppingCarts;
        }
        else
        {
            throw new NullReferenceException("Not found");
        }
    }

    public ShoppingCart GetShoppingCartByUserId(int userId)
    {
        var shoppingCart = _context.ShoppingCarts
            .Include(p => p.Products)
            .Include(p => p.User)
            .ThenInclude(u => u.Role)
            .Where(sc => sc.UserId == userId)
            .Where(sc => sc.Done == false)
            .SingleOrDefault();
        if (shoppingCart != null)
        {
            return shoppingCart;
        }
        else
        {
            throw new NullReferenceException("Not found");
        }
    }
}