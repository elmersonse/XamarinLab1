using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Product
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public float Price { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Product(string name, string producer, float price, DateTime expirationDate)
        {
            Name = name;
            Producer = producer;
            Price = price;
            ExpirationDate = expirationDate;
        }
    }
}
