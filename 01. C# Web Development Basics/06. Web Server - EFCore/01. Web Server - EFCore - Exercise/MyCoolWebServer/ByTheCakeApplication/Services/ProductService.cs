namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        public void Create(string name, decimal price, string imageUrl)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = new Product(name, price, imageUrl);

                db.Add(product);
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductListingViewModel> All(string searchTerm = null)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var products = db.Products.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products
                        .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
                }

                return products
                    .Select(x => new ProductListingViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price
                    }).ToList();
            }
        }

        public ProductDetailsViewModel GetById(int id)
        {
            using(var db = new ByTheCakeDbContext())
            {
                return db.Products
                    .Where(x => x.Id == id)
                    .Select(x => new ProductDetailsViewModel()
                    {
                        Name = x.Name,
                        Price = x.Price,
                        ImageUrl = x.ImageUrl
                    })
                    .FirstOrDefault();
            }
        }

        public bool Exists(int id)
        {
            using(var db = new ByTheCakeDbContext())
            {
                return db.Products.Any(x => x.Id == id);
            }
        }

        public IEnumerable<ProductCartViewModel> FindProductInCart(IEnumerable<int> ids)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db
                    .Products
                    .Where(x => ids.Contains(x.Id))
                    .Select(x => new ProductCartViewModel()
                    {
                        Name = x.Name,
                        Price = x.Price,
                    })
                    .ToList();
            }
        }
    }
}
