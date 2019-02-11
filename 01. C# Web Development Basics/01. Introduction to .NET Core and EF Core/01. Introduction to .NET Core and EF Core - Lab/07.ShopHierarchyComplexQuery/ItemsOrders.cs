namespace _07.ShopHierarchyComplexQuery
{
    public class ItemsOrders
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
