using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02.Panda.App.Models.Receipt
{
    public class ReceiptPackageViewModel
    {
        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }
    }
}
