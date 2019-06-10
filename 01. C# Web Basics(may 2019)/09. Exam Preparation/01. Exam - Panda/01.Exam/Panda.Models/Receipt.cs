﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Models
{
    public class Receipt
    {
        public Receipt()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Packages = new List<Package>();
        }

        [Key]
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        public string RecipientId { get; set; }
        public User Recipient { get; set; }

        [Required]
        public string PackageId  { get; set; }
        public virtual Package Package { get; set; }

        public virtual List<Package> Packages { get; set; }
    }
}
