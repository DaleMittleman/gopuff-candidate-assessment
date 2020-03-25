namespace Microservice.CartManager.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microservice.CartManager.Extensions;
    using Microservice.CartManager.Models;
    using Microservice.CartManager.Repositories;
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
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartsController"/> class.
        /// </summary>
        /// <param name="logger">A logger for this controller.</param>
        /// <param name="cache">The distributed cache (redis).</param>
        /// <param name="productRepository">The product repository.</param>
        public CartsController(
            ILogger<CartsController> logger,
            IDistributedCache cache,
            IProductRepository productRepository)
        {
            this.logger = logger;
            this.cache = cache;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Get a cart by id.
        /// </summary>
        /// <param name="cartId">The cart to get.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(Guid cartId)
        {
            this.logger.Log(LogLevel.Information, $"Called: GET /Carts/{cartId}");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            this.HttpContext.Response.Headers["Location"] = cart.Meta.Location;
            return this.Ok(cart);
        }

        /// <summary>
        /// Create a cart.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            this.logger.Log(LogLevel.Information, "Called: POST /Carts");

            var newCartId = Guid.NewGuid();
            var newCartLocation = this.Url.ActionLink("CreateCart", "Carts", new { cartId = newCartId });

            var cart = new Cart()
            {
                Id = newCartId,
                Status = CartStatus.New,
                Meta = new ApiResourceMetadata()
                {
                    Location = newCartLocation,
                },
            };

            // Push cart to cache
            await this.cache.SetAsync(cart.CacheKey, cart);

            // 201 Created
            // Sets location header
            return this.Created(newCartLocation, cart);
        }

        /// <summary>
        /// Update a cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCart([FromRoute] Guid cartId, [FromBody] Cart cart)
        {
            this.logger.Log(LogLevel.Information, $"Called: PUT /Carts/{cartId}");

            var oldCart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (oldCart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            if (cart.Status == CartStatus.Ordered)
            {
                return this.BadRequest($"Cannot alter already ordered cart {cartId}");
            }

            var cartModified = false;

            //// Check edits on the editable properties.

            var productIds = cart.ProductIds?.Where(this.productRepository.IsValidProductId) ?? new List<int>();
            if (productIds.Any())
            {
                oldCart.ProductIds = productIds;
                oldCart.Meta.LastModified = DateTime.Now;
                oldCart.Status = CartStatus.Active;
                cartModified = true;
            }

            if (cart.Recipient != null && !cart.Recipient.Equals(oldCart.Recipient))
            {
                oldCart.Recipient = cart.Recipient;
                oldCart.Meta.LastModified = DateTime.Now;
                cartModified = true;
            }

            if (cart.DeliveryAddress != null && !cart.DeliveryAddress.Equals(oldCart.DeliveryAddress))
            {
                oldCart.DeliveryAddress = cart.DeliveryAddress;
                oldCart.Meta.LastModified = DateTime.Now;
                cartModified = true;
            }

            if (cartModified)
            {
                // Push changes to cache
                await this.cache.SetAsync(oldCart.CacheKey, oldCart);

                this.HttpContext.Response.Headers["Location"] = this.Url.ActionLink("GetCart", "Carts", new { cartId });
                return this.Ok(oldCart);
            }
            else
            {
                return this.StatusCode(304, "No changes made");
            }
        }

        /// <summary>
        /// Delete a cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid cartId)
        {
            this.logger.Log(LogLevel.Information, $"Called: DELETE /Carts/{cartId}");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            if (cart.Status == CartStatus.Ordered)
            {
                return this.BadRequest($"Cannot delete already ordered cart {cartId}");
            }

            await this.cache.RemoveAsync(cart.CacheKey);
            return this.NoContent();
        }

        /// <summary>
        /// Add products to a cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="productIds">The product ids.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpPost("{cartId}/Products")]
        public async Task<IActionResult> AddProducts([FromRoute] Guid cartId, [FromBody] IEnumerable<int> productIds)
        {
            this.logger.Log(LogLevel.Information, $"Called: POST /Carts/{cartId}/Products");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            if (cart.Status == CartStatus.Ordered)
            {
                return this.BadRequest($"Cannot alter already ordered cart {cartId}");
            }

            var validProductIds = productIds?.Where(this.productRepository.IsValidProductId) ?? new List<int>();

            if (validProductIds.Any())
            {
                var cartProductIds = cart.ProductIds.ToList();
                cartProductIds.AddRange(validProductIds);

                cart.ProductIds = cartProductIds;

                cart.Status = CartStatus.Active;
                cart.Meta.LastModified = DateTime.UtcNow;
                return this.Ok(cart);
            }
            else
            {
                return this.StatusCode(304, "No valid product ids present in request body.");
            }
        }

        /// <summary>
        /// Removes a product from a cart.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="productId">The product id to remove.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpDelete("{cartId}/Products/{productId}")]
        public async Task<IActionResult> AddProducts([FromRoute] Guid cartId, [FromRoute] int productId)
        {
            this.logger.Log(LogLevel.Information, $"Called: DELETE /Carts/{cartId}/Products/{productId}");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            if (cart.Status == CartStatus.Ordered)
            {
                return this.BadRequest($"Cannot alter already ordered cart {cartId}");
            }

            if (cart.ProductIds != null && cart.ProductIds.Any(p => p == productId))
            {
                var cartProductIds = cart.ProductIds.ToList();

                // Remove first product id
                cartProductIds.Remove(productId);

                cart.ProductIds = cartProductIds;

                cart.Status = CartStatus.Active;
                cart.Meta.LastModified = DateTime.UtcNow;
                return this.Ok(cart);
            }
            else
            {
                return this.NotFound($"Cart {cartId} does not contain a product with id {productId}");
            }
        }

        /// <summary>
        /// Move a cart to check out.  Add validation for carts.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpPost("{cartId}/CheckOut")]
        public async Task<IActionResult> CheckOutCart([FromRoute] Guid cartId)
        {
            this.logger.Log(LogLevel.Information, $"Called: POST /Carts/{cartId}/CheckOut");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            if (cart.Status == CartStatus.Ordered)
            {
                return this.BadRequest($"Cannot checkout already ordered cart {cartId}");
            }

            //// Validation

            if (string.IsNullOrWhiteSpace(cart.Recipient))
            {
                return this.BadRequest("Cart must have a non-empty recipient name.");
            }

            if (string.IsNullOrWhiteSpace(cart.DeliveryAddress))
            {
                return this.BadRequest("Cart must have a non-empty delivery address.");
            }

            if (cart.ProductIds.Count() < 5)
            {
                return this.BadRequest("Cart must contain at least 5 products.");
            }

            var itemsPulledFromStock = new List<int>();
            foreach (var productId in cart.ProductIds)
            {
                if (!this.productRepository.IsValidProductId(productId))
                {
                    return this.BadRequest($"Product id {productId} is invalid.");
                }

                if (!this.productRepository.TryDepleteStock(productId))
                {
                    // Return pulled items
                    itemsPulledFromStock.ForEach(this.productRepository.IncreaseStock);

                    return this.BadRequest($"Not enough of product {productId} in stock");
                }
                else
                {
                    itemsPulledFromStock.Add(productId);
                }
            }

            cart.Status = CartStatus.CheckedOut;
            cart.Meta.LastModified = DateTime.UtcNow;

            // Push changes to cache.
            await this.cache.SetAsync(cart.CacheKey, cart);

            this.HttpContext.Response.Headers["Location"] = this.Url.ActionLink("GetCart", "Carts", new { cartId });
            return this.Ok(cart);
        }

        /// <summary>
        /// Move a cart to check out.  Add validation for carts.
        /// </summary>
        /// <param name="cartId">The cart id.</param>
        /// <param name="paymentInfo">The payment information.</param>
        /// <returns>A <see cref="IActionResult"/>.</returns>
        [HttpPost("{cartId}/Order")]
        public async Task<IActionResult> OrderCart([FromRoute] Guid cartId, [FromBody] PaymentInformation paymentInfo)
        {
            this.logger.Log(LogLevel.Information, $"Called: POST /Carts/{cartId}/CheckOut");

            var cart = await this.cache.GetAsync<Cart>(Cart.GenerateCacheKey(cartId));

            if (cart == null)
            {
                return this.NotFound($"Cart {cartId} not found.");
            }

            if (cart.Status != CartStatus.CheckedOut)
            {
                return this.BadRequest($"Cannot order a cart if it is not in a checked out state.");
            }

            if (paymentInfo.PaymentMethod == null || string.IsNullOrWhiteSpace(paymentInfo.BillingAddress))
            {
                return this.BadRequest($"Invalid or incomplete payment information.");
            }

            cart.Status = CartStatus.Ordered;
            cart.PaymentMethod = paymentInfo.PaymentMethod;
            cart.Meta.LastModified = DateTime.UtcNow;

            // Push changes to cache.
            await this.cache.SetAsync(cart.CacheKey, cart);

            this.HttpContext.Response.Headers["Location"] = this.Url.ActionLink("GetCart", "Carts", new { cartId });
            return this.Ok(cart);
        }
    }
}
