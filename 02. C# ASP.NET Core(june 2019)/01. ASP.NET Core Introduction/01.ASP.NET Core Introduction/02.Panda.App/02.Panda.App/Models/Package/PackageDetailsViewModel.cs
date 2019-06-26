using System;

namespace _02.Panda.App.Models.Package
{
    public class PackageDetailsViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public string Recipient { get; set; }
    }
}
