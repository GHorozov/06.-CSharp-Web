namespace MyCoolWebServer.ByTheCakeApplication.Services.Contracts
{
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null);

        ProductDetailsViewModel GetById(int id);

        bool Exists(int id);

        IEnumerable<ProductCartViewModel> FindProductInCart(IEnumerable<int> ids);
    }
}
