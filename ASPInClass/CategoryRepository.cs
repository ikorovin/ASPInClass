using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPInClass.Models;
using MySql.Data.MySqlClient;

namespace ASPInClass
{
    public class CategoryRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("connectionString.txt");

        public List<Category> GetCategories()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from Categories;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Product> allProducts = new List<Product>();

                {
                    var allCategories = new List<Category>();
                    while (reader.Read())
                    {
                        var currentCategory = new Category();
                        currentCategory.CategoryID = reader.GetInt32("CategoryID");
                        currentCategory.Name = reader.GetString("Name");
                        allCategories.Add(currentCategory);
                    }
                    return allCategories;
                }
            }
        }
    }
}
