namespace _02.Panda.App.Models.Package
{
    public class PackageCreateBindingModel
    {
        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Recipient { get; set; }
    }
}
