﻿using Dapper;
using Koton.ECommerce.Core.DTOs;
using Koton.ECommerce.DataAccess.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Koton.ECommerce.DataAccess.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;
        public ProductRepository(IConfiguration config)
        {
                _config = config;
        }
        public async Task<IEnumerable<GetProductDto>> GetProducts()
        {
            var connStr = _config.GetSection("ConnectionStrings:ConnStr").Value;

            using (var connection = new SqlConnection(connStr))
            {
                
                var products = await connection.QueryAsync<GetProductDto>("SELECT * FROM Products");

                return products;
            }
        }

        public async Task<GetProductDto> GetProductById(int id)

        {
            var connStr = _config.GetSection("ConnectionStrings:ConnStr").Value;
            using (var connection = new SqlConnection(connStr))
            {
                // Retrieve the product with the specified id.
                var product = await connection.QueryFirstOrDefaultAsync<GetProductDto>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });

                return product;
            }
        }

        public async Task<GetProductDto> AddProduct(CreateProductDto product)
        {
            var connStr = _config.GetSection("ConnectionStrings:ConnStr").Value;
            using (var connection = new SqlConnection(connStr))
            {
                
                var productId = await connection.ExecuteScalarAsync<int>("INSERT INTO Products (Id,Name,Price,DiscountPrice, Description,ImageUrl,Brand,UserId ) OUTPUT INSERTED.Id VALUES (@Id,@Name,@Price,@DiscountPrice, @Description,@ImageUrl,@Brand,@UserId);", product);


                // Retrieve the newly created product.
                var createdProduct = await GetProductById(productId);

                return createdProduct;
            }
        }

        public async Task<GetProductDto> UpdateProduct(int id, UpdateProductDto product)
        {
            var connStr = _config.GetSection("ConnectionStrings:ConnStr").Value;
            using (var connection = new SqlConnection(connStr))
            {
                
                await connection.ExecuteAsync("UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id",
                   new { Id = id, product.Name, product.Description, product.Price });

                
                var updatedProduct = await GetProductById(id);

                return updatedProduct;
            }
        }

        public async Task DeleteProduct(int id)
        {
            var connStr = _config.GetSection("ConnectionStrings:ConnStr").Value;
            using (var connection = new SqlConnection(connStr))
            {
                // Delete the product from the database.
                await connection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = id });
            }
        }


    }
}
