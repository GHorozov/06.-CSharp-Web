namespace _05.ShopHierarchy
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public Customer(string name, int salesmanId)
        {
            this.Name = name;
            this.SalesmanId = salesmanId;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int SalesmanId { get; set; }
        public Salesman Salesman { get; set; }
    }
}