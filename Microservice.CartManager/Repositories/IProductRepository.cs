namespace Microservice.CartManager.Repositories
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;
    using Microservice.CartManager.Models;

    /// <summary>
    /// Exposes operations against the product collection.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets all products in the collection.
        /// </summary>
        /// <param name="resultSizeCap">The result size cap.</param>
        /// <returns>The list of products and total count.</returns>
        (IEnumerable<Product>, int) GetAllProducts(int resultSizeCap = 30);

        /// <summary>
        /// Gets a product with a given id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The product with that id.</returns>
        Product GetProduct(int productId);

        /// <summary>
        /// Checks whether a provided id is a valid product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>A value indicating whether a product exists with that id.</returns>
        bool IsValidProductId(int productId);

        /// <summary>
        /// Deplete the stock of a product.
        /// </summary>
        /// <param name="productId">The product id to deplete.</param>
        /// <returns>A value indicating the stock could be depleted.</returns>
        bool TryDepleteStock(int productId);

        /// <summary>
        /// Increase the stock of a product.
        /// </summary>
        /// <param name="productId">The product id to add.</param>
        void IncreaseStock(int productId);
    }
}
