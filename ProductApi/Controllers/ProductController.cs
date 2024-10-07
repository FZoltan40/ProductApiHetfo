using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProductApi.Models;
using static ProductApi.Dtos.Dto;

namespace ProductApi.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Connect conn = new();

        [HttpGet]
        public List<Product> Get()
        {
            List<Product> products = new List<Product>();

            conn.Connection.Open();
            string sql = "SELECT * FROM products";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            do
            {
                var result = new Product
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Price = reader.GetInt32(2),
                    CreatedTime = reader.GetDateTime(3)
                };

                products.Add(result);
            } while (reader.Read());

            conn.Connection.Close();

            return products;
        }

        [HttpPost]
        public ActionResult<Product> Post(CreateProductDto product)
        {
            var result = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price,
                CreatedTime = DateTime.Now
            };

            string sql = $"INSERT INTO `products`(`Id`, `Name`, `Price`, `CreatedTime`) VALUES ('{result.Id}','{result.Name}', {result.Price}, '{result.CreatedTime.ToString("yyyy-MM-dd hh:mm:ss")}')";

            conn.Connection.Open();

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            return StatusCode(201, result);
        }
    }
}
