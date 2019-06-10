using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Models
{
    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Description  { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }
        public virtual User Recipient { get; set; }

        [Required]
        public string ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
