using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace EA_API
{
    public class ApiContext : DbContext
    {
        public DbSet<EA_ProductsInMemory> Products { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApiContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            LoadProducts();
        }
          

    public void LoadProducts()
        {
            EA_ProductsInMemory product = new EA_ProductsInMemory()
            {
                Name = "Product1",
                Price = 1005,
                CreatedDate = DateTime.Now.Date,
                Productdesc = "this is freehold"
            };
            Products.Add(product);
            EA_ProductsInMemory produt2 = new EA_ProductsInMemory()
            {
                Name = "Product2",
                Price = 2005,
                CreatedDate = DateTime.Now.Date,
                Productdesc = "this is Onhold"
            };
            Products.Add(produt2);
        }

        public List<EA_ProductsInMemory> GetProducts()
        {
            return Products.Local.ToList<EA_ProductsInMemory>();
        }
    }
}
