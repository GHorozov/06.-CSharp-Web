namespace MyCoolWebServer.ByTheCakeApplication.Models
{
    public class Cake
    {
        public Cake(int id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal  Price { get; set; }

        public override string ToString()
        {
            return $"{this.Name} - ${this.Price:f2}";
        }
    }
}
