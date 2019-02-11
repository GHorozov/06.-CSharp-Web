namespace _06.ShopHierarchyExtended
{
    public class Order
    {
        public Order(int customerId)
        {
            this.CustomerId = customerId;
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
