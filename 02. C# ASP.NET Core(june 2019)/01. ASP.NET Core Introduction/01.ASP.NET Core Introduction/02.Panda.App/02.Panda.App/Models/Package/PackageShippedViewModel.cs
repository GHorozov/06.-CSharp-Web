using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02.Panda.App.Models.Package
{
    public class PackageShippedViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string Recipient { get; set; }
    }
}
