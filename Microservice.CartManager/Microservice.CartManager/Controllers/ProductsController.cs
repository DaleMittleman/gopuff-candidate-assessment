namespace Microservice.CartManager.Controllers
{
    using System.Linq;
    using Microservice.CartManager.Models;
    using Microservice.CartManager.Repositories;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Exposes endpoints that act against the products collection.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Gets a specific product by id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>A <see cref="IActionResult"/> that wraps a product.</returns>
        [HttpGet("{productId}")]
        public IActionResult GetProduct([FromRoute] int productId)
        {
            var product = this.productRepository.GetProduct(productId);

            if (product != null)
            {
                var productLocationUrl = this.Url.ActionLink("GetProduct", "Products", new { productId });

                product.Meta.Location = productLocationUrl;
                this.HttpContext.Response.Headers["Location"] = productLocationUrl;

                return this.Ok(product);
            }
            else
            {
                return this.NotFound($"Product {productId} not found");
            }
        }

        /// <summary>
        /// Get all the products.  Page if needed.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> that wraps the search results.</returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var (products, totalResults) = this.productRepository.GetAllProducts();

            var searchMeta = new ProductSearchMeta()
            {
                ProductIds = string.Join(",", products.Select(p => p.ProductId)),
                PageSize = products.Count().ToString(),
                Page = new SearchResultsPage()
                {
                    TotalResults = totalResults,
                    PageSize = products.Count().ToString(),
                },
            };

            var results = new ProductSearchResults()
            {
                Products = products,
                SearchMeta = searchMeta,
            };

            return this.Ok(results);
        }
    }
}
