using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(100,200)]
        public decimal Cost { get; set; } 

        [Required]
        [Range(1,100)]
        public int Units { get; set; }
        public string Category { get; set; }
    }
}