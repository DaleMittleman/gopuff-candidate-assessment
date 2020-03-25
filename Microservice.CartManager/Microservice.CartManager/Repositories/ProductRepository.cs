namespace Microservice.CartManager.Repositories
{
    using System.IO;
    using System.Text.Json;
    using Microservice.CartManager.Models;
    using Microservice.CartManager.Utilities;

    /// <summary>
    /// In-memory implementation of the product repository.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ProductSearchResults productSearchResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
        {
            // Get products collection from file.
            this.productSearchResults = JsonSerializer.Deserialize<ProductSearchResults>(
                File.ReadAllText("../../products.json"),
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                });
        }

        /// <inheritdoc/>
        public Product GetProduct(int productId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public bool IsValidProductId(int productId)
        {
            throw new System.NotImplementedException();
        }
    }

}