using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApiProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Units { get; set; }
        public string Category { get; set; }
    }
}