namespace Microservice.CartManager.Models
{
    /// <summary>
    /// A product search results page.
    /// </summary>
    public class SearchResultsPage
    {
        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public string PageSize { get; set; }
    }
}
