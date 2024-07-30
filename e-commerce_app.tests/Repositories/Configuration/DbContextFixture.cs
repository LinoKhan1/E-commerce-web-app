using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Repositories.Configuration
{
    public class DbContextFixture : IDisposable
    {

        public ApplicationDbContext Context { get; private set; }

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB name for each instance
                .Options;

            Context = new ApplicationDbContext(options);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var categories = new List<Category>
        {
            new Category {CategoryId = 1, Name = "Electronic"},
            new Category {CategoryId = 2, Name = "Books" }
        };

            var products = new List<Product>
        {
            new Product {Id = 1, Name ="Laptop", Description="Test Description", Price=35099, Stock=50, CategoryId=1, Category = categories[0]},
            new Product {Id = 2, Name = "Smartphone", Description="Test Description", Price=35099, Stock=50, CategoryId= 1, Category = categories[0]},
            new Product {Id = 3, Name = "Novel", Description="Test Description", Price=349, Stock=50, CategoryId=2, Category = categories[1]}
        };
            var cartItems = new List<CartItem>
            {
                new CartItem {CartItemId = 1, UserId = "user1", ProductId = 1, Quantity = 2, DateAdded = DateTime.UtcNow,
                Product = products[0]},
                new CartItem {CartItemId = 2, UserId = "user2", ProductId = 2, Quantity = 1, DateAdded = DateTime.UtcNow,
                Product = products[1] }
            };

            Context.Categories.AddRange(categories);
            Context.Products.AddRange(products);
            Context.CartItems.AddRange(cartItems);

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
