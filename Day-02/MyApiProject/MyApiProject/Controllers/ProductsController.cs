﻿using MyApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApiProject.Controllers
{
    public class ProductsController : ApiController
    {
        private static List<Product> data = new List<Product> {
            new Product { Id = 3, Name = "Pen", Cost = 5, Units = 50, Category = "Stationary" },
            new Product { Id = 5, Name = "Len", Cost = 50, Units = 30, Category = "Grocery" },
            new Product { Id = 2, Name = "Ten", Cost = 10, Units = 60, Category = "Stationary" },
            new Product { Id = 9, Name = "Den", Cost = 25, Units = 20, Category = "Grocery" },
            new Product { Id = 7, Name = "Zen", Cost = 35, Units = 30, Category = "Stationary" }
        };

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return data;
        }

        public Product Get(int id)
        {
            var result = data.Find(product => product.Id == id);
            if (result != null)
                return result;
            var errorMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            errorMessage.Content = new StringContent("Invalid id");
            errorMessage.ReasonPhrase = "Not Found";
            throw new HttpResponseException(errorMessage);
        }

        public Product Post(Product product)
        {
            data.Add(product);
            return product;
        }

        public Product Put(int id, Product product)
        {
            var result = data.Find(productObj => productObj.Id == id);
            if (result == null)
            {
                var errorMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                errorMessage.Content = new StringContent("Invalid id");
                errorMessage.ReasonPhrase = "Not Found";
                throw new HttpResponseException(errorMessage);
            }
            data.Remove(result);
            data.Add(product);
            return product;
            
        }
    }
}