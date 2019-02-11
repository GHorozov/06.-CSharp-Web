namespace _09.QueryLoadedData
{
    using System.Collections.Generic;

    public class Order
    {
        public Order(int customerId)
        {
            this.CustomerId = customerId;
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<ItemsOrders> ItemsOrders { get; set; } = new List<ItemsOrders>();
    }
}
