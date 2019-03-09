namespace _01.FirstWebSite_ByTheCakeApp.Application.Models
{
    using System;

    public class Cake
    {
        public Cake(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Name}{Environment.NewLine}Price: {this.Price:f2}";
        }
    }
}
