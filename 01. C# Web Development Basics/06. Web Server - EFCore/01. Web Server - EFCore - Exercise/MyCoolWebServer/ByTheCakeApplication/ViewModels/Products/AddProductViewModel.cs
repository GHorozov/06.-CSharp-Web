namespace MyCoolWebServer.ByTheCakeApplication.ViewModels.Products
{
    public class AddProductViewModel
    {
        public AddProductViewModel()
        {

        }

        public AddProductViewModel(int id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal  Price { get; set; }

        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return $"{this.Name} - ${this.Price:f2}";
        }
    }
}
