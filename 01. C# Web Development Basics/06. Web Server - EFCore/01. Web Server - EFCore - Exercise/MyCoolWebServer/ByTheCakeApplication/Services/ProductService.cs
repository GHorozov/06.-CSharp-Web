namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;

    public class ProductService : IProductService
    {
        public void Create(string name, decimal price, string imageUrl)
        {
            using(var db = new ByTheCakeDbContext())
            {
                var product = new Product(name, price, imageUrl);

                db.Add(product);
                db.SaveChanges();
            }
        }
    }
}
