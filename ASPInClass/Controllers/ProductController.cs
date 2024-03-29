﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPInClass.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPInClass.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var repo = new ProductRepository();
            var products = repo.GetAllProducts();
            return View(products);
        }

        public IActionResult ViewProduct(int id)
        {
            var repo = new ProductRepository();
            var product = repo.GetProduct(id);

            return View(product);
        }

        public IActionResult UpdateProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Product prod = repo.GetProduct(id);

            repo.UpdateProduct(prod);

            if (prod == null)
            {
                return View("ProductNotFound");
            }

            return View(prod);
        }

        public IActionResult UpdateProductToDatabase(Product product)
        {
            ProductRepository repo = new ProductRepository();

            repo.UpdateProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ProductID });

        }

        public IActionResult InsertProduct()
        {
            var repo = new ProductRepository();
            var prod = repo.AssignCategories();

            return View(prod);
        }

        public IActionResult InsertProductToDatabase(Product productToInsert)
        {
            var repo = new ProductRepository();
            repo.InsertProduct(productToInsert);
            return RedirectToAction("Index");
        }


        public IActionResult DeleteProduct(int id)
        {
            var repo = new ProductRepository();

            repo.DeleteProduct(id);

            return RedirectToAction("Index");
        }
    }
}