namespace _02.Panda.App.Models.Package
{
    public class PackageDeliveredViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Recipient { get; set; }
    }
}
