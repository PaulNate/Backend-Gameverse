using Gameverse.Models;

namespace Gameverse.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GameverseContext context)
        {

            if (context.Users.Any()
                && context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            var adminRole = new Role { Name = "Admin"};
            var customerRole = new Role { Name = "Customer" };

            var users = new User[]
            {
                new User
                { 
                    Name = "Paul Nate",
                    Email = "paulnate80@yahoo.com",
                    Password = "test123",
                    Address = "Nirajului 22",
                    Phone = "0746056216",
                    Role = adminRole
                },
                new User
                { 
                    Name = "George B",
                    Email = "georgeb35@gmail.com",
                    Password = "test123",
                    Address = "Mehedinti 22",
                    Phone = "0755056216",
                    Role = adminRole
                },
                new User
                { 
                    Name = "Florina G",
                    Email = "florina@gmail.com",
                    Password = "test123",
                    Address = "Mehedinti 22",
                    Phone = "0755056216",
                    Role = customerRole
                },
                new User
                { 
                    Name = "Radu B",
                    Email = "radub@gmail.com",
                    Password = "test123",
                    Address = "Mehedinti 22",
                    Phone = "0755056216",
                    Role = customerRole
                },
                new User
                { 
                    Name = "Andrei D",
                    Email = "dumi@gmail.com",
                    Password = "test123",
                    Address = "Mehedinti 22",
                    Phone = "0755056216",
                    Role = customerRole
                }
            };

            context.Users.AddRange(users);

            var gameCategory = new Category { Name="Game"};
            var mediaCategory = new Category { Name="Media"};

            var reviews = new Review[]
            {
                new Review
                {
                    Grade = 3,
                    ReviewText = "Good",
                },
                new Review
                {
                    Grade = 5,
                    ReviewText = "Amazing!",   
                },
                new Review
                {
                    Grade = 5,
                    ReviewText = "Amazing gameplay!",
                },
                new Review
                {
                    Grade = 4,
                    ReviewText = "Not Bad",
                },
                new Review
                {
                    Grade = 1,
                    ReviewText = "Very Bad",
                }
            };


            var products = new Product[]{
                new Product
                {
                    Name = "Elden Ring",
                    Quantity = 100,
                    Description = "Elden Ring is an action role-playing game developed by FromSoftware and published by Bandai Namco Entertainment. The game was directed by Hidetaka Miyazaki and made in collaboration with fantasy novelist George R. R. Martin, who provided material for the game's setting. It was released for Microsoft Windows, PlayStation 4, PlayStation 5, Xbox One, and Xbox Series X/S on February 25, 2022.",
                    Price = 59.99,
                    Category = gameCategory,
                    Reviews = reviews,
                    ImageUrl = "https://fv9-3.failiem.lv/thumb_show.php?i=2bmx72fwf&view"
                },
                new Product
                {
                    Name = "Pirates and Knights",
                    Quantity = 100,
                    Description = "Pirates and Knights is an adventure role-playing game developed by NP Studios.",
                    Price = 20.00,
                    Category = gameCategory,
                    ImageUrl = "https://fv9-4.failiem.lv/thumb_show.php?i=sgb7f985k&view"
                },
                new Product
                {
                    Name = "Sekiro: Shadows die twice",
                    Quantity = 100,
                    Description = "Sekiro: Shadows Die Twice[a] is a 2019 action-adventure game developed by FromSoftware and published by Activision. The game follows a shinobi known as Wolf as he attempts to take revenge on a samurai clan who attacked him and kidnapped his lord. It was released for Microsoft Windows, PlayStation 4, and Xbox One in March 2019 and for Stadia in October 2020. "
                    +"Gameplay is focused on stealth, exploration, and combat, with a particular emphasis on boss battles. The game takes place in a fictionalized Japan during the Sengoku period and makes strong references to Buddhist mythology and philosophy. While making the game, lead director Hidetaka Miyazaki wanted to create a new intellectual property (IP) that marked a departure from the Souls series of games also made by FromSoftware. The developers looked to games such as The Mysterious Murasame Castle and the Tenchu series for inspiration."
                    +"Sekiro was praised by critics, who complimented its gameplay and setting, and compared it to the Souls games, although opinions on its difficulty were mixed. It was nominated for various awards and won several, including The Game Award for Game of the Year. The game sold over five million copies by July 2020.",
                    Price = 30.00,
                    Category = gameCategory,
                    ImageUrl = "https://fv9-3.failiem.lv/thumb_show.php?i=2en8tjmkg&view"
                },
                new Product
                {
                    Name = "The Joker",
                    Quantity = 100,
                    Description = "The Joker is a great game",
                    Price = 25.00,
                    Category = gameCategory,
                    ImageUrl = "https://fv9-4.failiem.lv/thumb_show.php?i=qq23fcheq&view"
                },
                new Product
                {
                    Name = "Stormveil Castle",
                    Quantity = 10,
                    Description = "A picture of a very cool castle",
                    Price = 10.00,
                    Category = mediaCategory,
                    ImageUrl = "https://fv9-4.failiem.lv/thumb_show.php?i=cjtebbahw&view"
                },
                new Product
                {
                    Name = "Clocktower",
                    Quantity = 10,
                    Description = "A picture of a very cool Clocktower",
                    Price = 20.00,
                    Category = mediaCategory,
                    ImageUrl = "https://fv9-5.failiem.lv/thumb_show.php?i=2bnjfxrxq&view"
                },
                new Product
                {
                    Name = "Flying bird",
                    Quantity = 12,
                    Description = "A picture of a very cool bird",
                    Price = 10.00,
                    Category = mediaCategory,
                    ImageUrl = "https://fv9-2.failiem.lv/thumb_show.php?i=b9r27dmsj&view"
                },
                new Product
                {
                    Name = "Ruined Manor",
                    Quantity = 1,
                    Description = "A picture of a ruined manor",
                    Price = 15.00,
                    Category = mediaCategory,
                    ImageUrl = "https://fv9-2.failiem.lv/thumb_show.php?i=w7untvah4&view"
                },
                new Product
                {
                    Name = "Pirate battle",
                    Quantity = 5,
                    Description = "A picture of a pirate battle",
                    Price = 15.00,
                    Category = mediaCategory,
                    ImageUrl = "https://fv9-5.failiem.lv/thumb_show.php?i=tj9hfemss&view"
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}