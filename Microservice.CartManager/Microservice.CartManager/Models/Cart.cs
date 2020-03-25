namespace Microservice.CartManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A e-shopping cart.
    /// </summary>
    public class Cart : ApiResource
    {
        /// <summary>
        /// Gets or sets the cart id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the list of product ids.
        /// </summary>
        public IEnumerable<int> ProductIds { get; set; }

        /// <summary>
        /// Gets or sets the recipient name.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the delivery address.
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        public PaymentMethod? PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the cart status.
        /// </summary>
        public CartStatus Status { get; set; }

        /// <summary>
        /// Gets the cache key for this cart.
        /// </summary>
        [JsonIgnore]
        public string CacheKey => string.Join(":", "cart", this.Id).ToUpperInvariant();

        /// <summary>
        /// Gets a cart cache key given an id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The cache key.</returns>
        public static string GenerateCacheKey(Guid id) => string.Join(":", "cart", id).ToUpperInvariant();
    }
}