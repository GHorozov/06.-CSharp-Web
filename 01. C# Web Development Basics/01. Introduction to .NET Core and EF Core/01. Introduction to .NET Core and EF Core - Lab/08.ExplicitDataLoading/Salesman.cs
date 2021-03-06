﻿namespace _08.ExplicitDataLoading
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Salesman
    {
        public Salesman(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}