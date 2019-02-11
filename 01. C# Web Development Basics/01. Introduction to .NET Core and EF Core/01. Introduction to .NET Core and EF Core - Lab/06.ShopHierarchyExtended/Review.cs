namespace _06.ShopHierarchyExtended
{
    public class Review
    {
        public Review(int customerId)
        {
            this.CustomerId = customerId;
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
