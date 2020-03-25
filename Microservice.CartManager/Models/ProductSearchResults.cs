namespace Microservice.CartManager.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// A model for the product search results contained within products.json.
    /// </summary>
    public class ProductSearchResults
    {
        /// <summary>
        /// Gets or sets the search results metadata.
        /// </summary>
        public ProductSearchMeta SearchMeta { get; set; }

        /// <summary>
        /// Gets or sets the product list.
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
    }
}