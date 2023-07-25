#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace SaintBarnabasHouse.Models;

public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    
    // We need to create a new DbSet<Model> for every model in our project that is making a table
    // The name of our table in our database will be based on the name we provide here
    // This is where we provide a plural version of our model to fit table naming standards    
    public DbSet<User> Users { get; set; } 
    public DbSet<Product> Products { get; set; } 
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategoryAssoc> ProductCategoryAssocs { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ProductImageAssoc> ProductImageAssocs { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CartProductAssoc> CartProductAssocs { get; set; }
    public DbSet<OrderProductAssoc> OrderProductAssocs { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    
}
