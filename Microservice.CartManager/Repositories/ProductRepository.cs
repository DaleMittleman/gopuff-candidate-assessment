namespace Microservice.CartManager.Repositories
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using Microservice.CartManager.Models;
    using Microservice.CartManager.Utilities;

    /// <summary>
    /// In-memory implementation of the product repository.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly IEnumerable<Product> products;
        private readonly HashSet<int> validProductIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
        {
            // Get products collection from file.
            var productSearchResults = JsonSerializer.Deserialize<ProductSearchResults>(
                File.ReadAllText("../products.json"),
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                });

            this.products = productSearchResults.Products;
            this.validProductIds = this.products.Select(p => p.ProductId).ToHashSet();
        }

        /// <inheritdoc/>
        public (IEnumerable<Product>, int) GetAllProducts(int resultSizeCap = 30)
        {
            return (this.products.Take(resultSizeCap), this.products.Count());
        }

        /// <inheritdoc/>
        public Product GetProduct(int productId)
        {
            return this.products.FirstOrDefault(product => product.ProductId == productId);
        }

        /// <inheritdoc/>
        public bool IsValidProductId(int productId)
        {
            return this.validProductIds.Contains(productId);
        }

        /// <inheritdoc/>
        public bool TryDepleteStock(int productId)
        {
            var product = this.products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return false;
            }

            product.Quantity -= 1;
            return true;
        }

        /// <inheritdoc/>
        public void IncreaseStock(int productId)
        {
            var product = this.products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return;
            }

            product.Quantity += 1;
        }
    }
}