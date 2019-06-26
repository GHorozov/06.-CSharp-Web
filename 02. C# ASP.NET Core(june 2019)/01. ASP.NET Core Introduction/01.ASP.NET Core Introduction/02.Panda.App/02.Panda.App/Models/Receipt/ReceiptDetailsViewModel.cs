using Panda.Domain;
using System;

namespace _02.Panda.App.Models.Receipt
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Recipient { get; set; }

        public ReceiptPackageViewModel Package { get; set; }
    }
}
