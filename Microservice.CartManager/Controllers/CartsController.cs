namespace Microservice.CartManager.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microservice.CartManager.Extensions;
    using Microservice.CartManager.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Exposes operations on the Carts collection.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> logger;
        private readonly IDistributedCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartsController"/> class.
        /// </summary>
        /// <param name="logger">A logger for this controller.</param>
        /// <param name="cache">The distributed cache (redis).</param>
        public CartsController(
            ILogger<CartsController> logger,
            IDistributedCache cache)
        {
            this.logger = logger;
            this.cache = cache;
        }

        /// <summary>
        /// Get all carts.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult GetAllCarts()
        {
            this.logger.Log(LogLevel.Information, "Called: GET /Carts");

            return this.Ok();
        }

        /// <summary>
        /// Get a cart by id.
        /// </summary>
        /// <param name="cartId">The cart to get.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpGet("{cartId}")]
        public IActionResult GetCart(Guid cartId)
        {
            this.logger.Log(LogLevel.Information, "Called: GET /Carts");

            return this.Ok();
        }

        /// <summary>
        /// Create a cart.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public IActionResult CreateCart()
        {
            this.logger.Log(LogLevel.Information, "Called: GET /Carts");

            return this.Ok();
        }

        /// <summary>
        /// Delete a cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid cartId)
        {
            this.logger.Log(LogLevel.Information, $"Called: Delete /Carts/{cartId}");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }
            else
            {
                await this.cache.RemoveAsync(cart.CacheKey);
                return this.NoContent();
            }
        }
    }
}
