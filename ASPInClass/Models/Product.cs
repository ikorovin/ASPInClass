using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPInClass.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OnSale { get; set; }
        public string StockLevel { get; set; }
        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }
    }
}
