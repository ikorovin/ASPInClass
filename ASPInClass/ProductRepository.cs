using ASPInClass.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ASPInClass
{
    public class ProductRepository
    {
        private static string connectionString = "Server=localhost;Database=bestbuy;uid=root;Pwd=password";
        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from Products;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Product> allProducts = new List<Product>();
                while (reader.Read() == true)
                {
                    var currentProduct = new Product();

                    currentProduct.ProductID = reader.GetInt32("ProductID");
                    currentProduct.Name = reader.GetString("Name");
                    currentProduct.Price = reader.GetDecimal("Price");

                    allProducts.Add(currentProduct);

                }

                return allProducts;
            }
        }

        public Product GetProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "Select * From Products WHERE ProductID = @ProductID;";
            cmd.Parameters.AddWithValue("ProductID", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var product = new Product();

                while (reader.Read() == true)
                {
                    product.ProductID = reader.GetInt32("ProductID");
                    product.Name = reader.GetString("Name");
                    product.Price = reader.GetDecimal("Price");
                    product.CategoryID = reader.GetInt32("CategoryID");
                    product.OnSale = reader.GetInt32("OnSale");

                    if (reader.IsDBNull(reader.GetOrdinal("Stocklevel")))
                    {
                        product.StockLevel = null;
                    }
                    else
                    {
                        product.StockLevel = reader.GetString("StockLevel");
                    }

                }
                return product;
            }
        }

        public void UpdateProduct(Product productToUpdate)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "Update products SET Name = @name,Price = @price WHERE ProductID = @id;";
            cmd.Parameters.AddWithValue("name", productToUpdate.Name);
            cmd.Parameters.AddWithValue("price", productToUpdate.Price);
            cmd.Parameters.AddWithValue("id", productToUpdate.ProductID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertProduct(Product productToInsert)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "Insert into Products (Name,Price,CategoryID) Values (@name,@price,@categoryID)";
            cmd.Parameters.AddWithValue("name", productToInsert.Name);
            cmd.Parameters.AddWithValue("price", productToInsert.Price);
            cmd.Parameters.AddWithValue("categoryID", productToInsert.CategoryID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Product AssignCategories()
        {
            var catRepo = new CategoryRepository();
            var catList = catRepo.GetCategories();
            Product product = new Product();
            product.Categories = catList;
            return product;
        }

        public void DeleteProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "Delete FROM products WHERE ProductID = @id";

            cmd.Parameters.AddWithValue("id", id);


            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
