namespace Microservice.CartManager.Models
{
    /// <summary>
    /// Product search results metadata.
    /// </summary>
    public class ProductSearchMeta
    {
        /// <summary>
        /// Gets or sets the product ids.
        /// </summary>
        public string ProductIds { get; set; }

        /// <summary>
        /// Gets or sets the location id.
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public string PageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tobacco products were filtered out.
        /// </summary>
        public bool BlockTobacco { get; set; }

        /// <summary>
        /// Gets or sets the callout category.
        /// </summary>
        public string CalloutCategory { get; set; }

        /// <summary>
        /// Gets or sets the page description.
        /// </summary>
        public SearchResultsPage Page { get; set; }
    }
}
