using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Category = new List<string> { "Category A" },
                    Description = "Description for Product 1",
                    ImageFile = "product1.jpg",
                    Price = 100.00m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Category = new List<string> { "Category B" },
                    Description = "Description for Product 2",
                    ImageFile = "product2.jpg",
                    Price = 200.00m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Category = new List<string> { "Category C" },
                    Description = "Description for Product 3",
                    ImageFile = "product3.jpg",
                    Price = 150.00m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    Category = new List<string> { "Category A", "Category D" },
                    Description = "Description for Product 4",
                    ImageFile = "product4.jpg",
                    Price = 250.00m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 5",
                    Category = new List<string> { "Category E" },
                    Description = "Description for Product 5",
                    ImageFile = "product5.jpg",
                    Price = 175.00m
                }
            };
        }
    }
}
