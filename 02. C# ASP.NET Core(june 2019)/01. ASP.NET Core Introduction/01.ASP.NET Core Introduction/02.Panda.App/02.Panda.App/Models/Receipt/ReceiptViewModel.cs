using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02.Panda.App.Models.Receipt
{
    public class ReceiptViewModel
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Recipient { get; set; }
    }
}
